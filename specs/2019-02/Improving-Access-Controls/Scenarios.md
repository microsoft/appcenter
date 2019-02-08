# Overview

App Center has roles that can only be applied broadly across all the services within the portal and through the API's. There are currently 4 roles that define what a user can do with an app's configuration and data, but we frequently hear requests for more options.

## Built-in Roles Today

There are currently two categories of roles within App Center. There are Organization level roles and Team roles.

### Organization Level

* Admins can manage the settings of an organization (change its name, permissions, set up billing). They also automatically have 'Manager' level app permissions for all apps within the Org
* Collaborators are able to create apps, also making them the manager of that app, create teams within an Organization. Collaborators can work within any apps they are members of.

### Team Level

Teams exist within the context of an Organization, and a single organization can have multiple teams. We currently have four defined team roles.

* Managers can manage app settings, collaborators, and integrations
* Developers can manage app services (e.g. create builds, run tests)
* Testers can install apps they are distribution group members of
* Viewers access is limited to a read-only state

# Goals

Introduce new role based access controls for App Center services, user scenarios and functionality with a strong focus on ease of use over maximum granularity.

# Scenarios

## Access

* Admins can add Azure Active Directory groups as an Org collaborator, granting, managing, and removing access through existing security groups within the security group

## Roles

1. Admins can create, update and delete additional roles
2. Admins cannot create, update and delete the 4 built-in default roles
3. Admins can edit a roles permissions

## Role Permissions

Custom roles will include the ability to include or exclude access to a specific service along with a permission set focused on functionality within the service itself.

### Build

 1. Can create build configuration
 2. Can delete build configuration
 3. Can start a build
 4. Can delete a build
 5. Can view build logs

### Test

  1. Can start test run
  2. Can view test run
  3. Can delete test run
  
### Distribution

  1. Can distribute release
  2. Can manage distribution groups
  3. Can delete release
  
### Diagnostics

  1. Can delete issues
  
### Analytics

  1. Can view analytics
  2. Can manage export
  
### Push

  1. Can send push messages

### Identity

  1. Can create identity tenant
  2. Can configure identity tenant
  3. Can delete identity tenant
  4. Can view user details
  
# GDPR

  1. Can submit DSR delete requests
  2. Can submit DSR export requests 

## Teams

With an existing `Team` structure already in place the assumption is this serves as a logical fit for security group management.

1. Admins can create a `Team` and limit the services members have access to see
2. Admins can create a `Team` and limit functionality within a service members have access to use

## App Security

1. Admins can lock down an app to users belonging to a specific corporate domain
2. Admins can lock down an org to users belonging to a specific corporate domain
3. Admins can be secure app releases behind an authenticated web address

## Audit

1. Admins, developers and viewers can view an audit trail of actions performed by users within an app or org
