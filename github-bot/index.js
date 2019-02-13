const getConfig = require('probot-config')
const createScheduler = require('probot-scheduler')
const Welcome = require('./lib/welcome')
const Stale = require('./lib/stale')

module.exports = async app => {
  // Visit all repositories to mark and sweep stale issues
  const scheduler = createScheduler(app)

  // Unmark stale issues if a user comments
  const events = [
    'issue_comment',
    'issues',
    'pull_request',
    'pull_request_review',
    'pull_request_review_comment'
  ]
  app.on(events, unmark)
  app.on('schedule.repository', markAndSweep)

  // Welcome the user and classify the issue
  app.on(['issues'], welcomeMessage)

  // Close issues that haven't received a response

  // Close stale issues

  async function unmark (context) {
    if (!context.isBot) {
      const stale = await forRepository(context)
      let issue = context.payload.issue || context.payload.pull_request
      const type = context.payload.issue ? 'issues' : 'pulls'

      // Some payloads don't include labels
      if (!issue.labels) {
        try {
          issue = (await context.github.issues.get(context.issue())).data
        } catch (error) {
          context.log('Issue not found')
        }
      }

      const staleLabelAdded = context.payload.action === 'labeled' &&
        context.payload.label.name === stale.config.staleLabel

      if (stale.hasStaleLabel(type, issue) && issue.state !== 'closed' && !staleLabelAdded) {
        stale.unmark(type, issue)
      }
    }
  }

  async function welcomeMessage(context) {
    const welcome = await forRepositoryWelcome(context)
    let issue = context.payload.issue || context.payload.pull_request
    const type = context.payload.issue ? 'issues' : 'pulls'

    await welcome.message(context.payload);
  }

  async function markAndSweep (context) {
    const stale = await forRepository(context)
    await stale.markAndSweep('pulls')
    await stale.markAndSweep('issues')
  }

  async function forRepository (context) {
    let config = await getConfig(context, 'appcenter-bot.yml')

    if (!config) {
      scheduler.stop(context.payload.repository)
      // Don't actually perform for repository without a config
      config = { perform: false }
    }

    config = Object.assign(config, context.repo({ logger: app.log }))

    return new Stale(context.github, config)
  }

  async function forRepositoryWelcome (context) {
    let config = await getConfig(context, 'appcenter-bot.yml')

    if (!config) {
      scheduler.stop(context.payload.repository)
      // Don't actually perform for repository without a config
      config = { perform: false }
    }

    config = Object.assign(config, context.repo({ logger: app.log }))

    return new Welcome(context.github, config)
  }
}
