const schema = require('./schema')

module.exports = class Welcome {
  constructor (github, { owner, repo, logger = console, ...config }) {
    this.github = github
    this.logger = logger

    const { error, value } = schema.validate(config)

    this.config = value
    if (error) {
      // Report errors to sentry
      logger.warn({ err: new Error(error), owner, repo }, 'Invalid config')
    }

    Object.assign(this.config, { owner, repo })
  }

  async message (context) {
    const featureFirstComment = this.config['featureFirstComment']
    const bugFirstComment = this.config['bugFirstComment']
    const { owner, repo } = this.config
    const { event } = this.github

    console.log(context)
    if (event === "issues.opened") {
      await this.github.issues.createComment({ owner, repo, context.issue.number, body: featureFirstComment })
      this.logger.info('%s/%s#%d would have been marked (dry-run)', owner, repo, number)
    }
  }

}
