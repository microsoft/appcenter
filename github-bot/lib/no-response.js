const scramjet = require('scramjet')

module.exports = class NoResponse {
  constructor (context, config, logger) {
    this.context = context
    this.github = context.github
    this.config = config
    this.logger = logger
  }

  async sweep () {
    this.logger.debug('Starting sweep')

    await this.ensureLabelExists(this.context.repo({name: this.config.responseRequiredLabel, color: this.config.responseRequiredColor}))

    const issues = await this.getClosableIssues()
    issues.forEach(issue => this.close(this.context.repo({number: issue.number})))
  }

  async unmark (issue) {
    const {perform, responseRequiredLabel} = this.config
    const {owner, repo, number} = issue
    const comment = this.context.payload.comment

    const issueInfo = await this.github.issues.get(issue)

    const isMarked = await this.hasResponseRequiredLabel(issue)
    if (isMarked && issueInfo.data.user.login === comment.user.login) {
      if (perform) {
        this.logger.info('%s/%s#%d is being unmarked', owner, repo, number)
        await this.github.issues.removeLabel({owner, repo, number, name: responseRequiredLabel})
        if (issueInfo.data.state === 'closed' && issueInfo.data.user.login !== issueInfo.data.closed_by.login) {
          this.github.issues.edit({owner, repo, number, state: 'open'})
        }
      } else {
        this.logger.info('%s/%s#%d would have been unmarked (dry-run)', owner, repo, number)
      }
    }
  }

  async close (issue) {
    const {closeComment, perform} = this.config

    if (perform) {
      this.logger.info('%s/%s#%d is being closed', issue.owner, issue.repo, issue.number)
      if (closeComment) {
        await this.github.issues.createComment(Object.assign({}, issue, {body: closeComment}))
      }
      return this.github.issues.edit(Object.assign({}, issue, {state: 'closed'}))
    } else {
      this.logger.info('%s/%s#%d would have been closed (dry-run)', issue.owner, issue.repo, issue.number)
    }
  }

  async ensureLabelExists ({name, color}) {
    return this.github.issues.getLabel(this.context.repo({name})).catch(() => {
      return this.github.issues.createLabel(this.context.repo({name, color}))
    })
  }

  async findLastLabeledEvent (owner, repo, number) {
    const {responseRequiredLabel} = this.config
    const params = {owner, repo, issue_number: number, per_page: 100}
    const events = await this.github.paginate(this.github.issues.getEvents(params))
    return events[0].data.reverse()
                 .find(event => event.event === 'labeled' && event.label.name === responseRequiredLabel);
  }

  async getClosableIssues () {
    const {owner, repo} = this.context.repo()
    const {daysUntilClose, responseRequiredLabel} = this.config
    const q = `repo:${owner}/${repo} is:issue is:open label:"${responseRequiredLabel}"`
    const params = {q, sort: 'updated', order: 'desc', per_page: 30}
    const labeledEarlierThan = this.since(daysUntilClose)

    const issues = await this.github.search.issues(params)
    const closableIssues = scramjet.fromArray(issues.data.items).filter(async issue => {
      const event = await this.findLastLabeledEvent(owner, repo, issue.number)
      const creationDate = new Date(event.created_at)

      return creationDate < labeledEarlierThan
    }).toArray()
    return closableIssues
  }

  async hasResponseRequiredLabel (issue) {
    const labels = await this.github.issues.getIssueLabels(issue)

    return labels.data.map(label => label.name).includes(this.config.responseRequiredLabel)
  }

  since (days) {
    const ttl = days * 24 * 60 * 60 * 1000
    return new Date(new Date() - ttl)
  }
}
