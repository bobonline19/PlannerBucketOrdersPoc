# PlannerBucketOrdersPoc
A proof of concept using a planner board to track order fulfillment by moving tasks through buckets
<picture>
  <img alt="The planner that is monitored by the code" src="https://github.com/bobonline19/PlannerBucketOrdersPoc/blob/798a9cca639ea6aff7fd5c2ea789331b7de0cb00/docs/Planner.jpeg">
</picture>

Currently it can be run as a scheduled task on a computer to post a message to the Teams channel when a task changes bucket.
<picture>
  <img alt="A teams message because a task has changed bucket" src="https://github.com/bobonline19/PlannerBucketOrdersPoc/blob/798a9cca639ea6aff7fd5c2ea789331b7de0cb00/docs/change%20notification.png">
</picture>

# Idea
- Team of people are working on order fulfillment
- Each stage the order moves bucket
  - Open (An order has been placed but cannot be fulfilled. Need to make or acquire more stock)
  - Allocated (Order is ready to be packed)
  - Picked (It is boxed and ready to go. Book courier weights and dimensions)
  - In Transit (Courier has collected goods, record tracking number - microsoft list?)
  - Delivered (Arrived at destination)
 
 # Why Planner
 - Count of items per bucket and calendar views give good summary of order status (specify due dates, labels)
 - Attach an Excel or PDF version of the pick list, packing note, photos of items being packed devilvered collected (attachments all in one place
  - Could do with a function to tidy task attachments created with planner UI into an attachments folder, possibly with sub folders
 - Planner mobile app can move task between buckets and attach photos / documents and update task
 - Can surface the plan in teams for everyone to monitor on the go (e.g. managers) without another app to install
 
 # Planned Extensions
 - Notify on bucket change
  - Different team members are responsible for each bucket. As a task appears in their bucket a teams message appears in their channel as an action
    - Need to identify when a task changes bucket
    - Can use incoming webhook but would prefer bot
- Tidy up attachments
  - Currently attachments are root of document library can we move them to an attachments folder to keep root tidy
 
 # Where we have got to
 ## Setup
 - Create sandabox instance to play with
 - Register an (native) app and get client ID
 - Create a team and note the ~Config.GroupName~
 - Create a Planner plan for the team and note the ~Config.PlanName~
 - Create a channel for the team and register an [Incoming Web Hook](https://learn.microsoft.com/en-us/microsoftteams/platform/webhooks-and-connectors/how-to/add-incoming-webhook)
 - Create an ISecrets implementatoion with app registered ClientID and WebHookURL (not checked in)
 
 ## What Happens
 - Each time the console app is run the plan tasks are captured to a json file as a snapshot
 - If there is a previous file the snapshot is compared to the previous file
 - If the bucket ID of a task does not match the previous bucket ID a message is posted to
  - the console
  - the webhook

## ToDo
- Store the snapshot file to the team drive not locally
- Convert the whole thing to a Web API project or Azure function
- Report the changes with an incrementing ID
- Using a polling Power Automate Custom Trigger to spot the new Change ID and trigger a flow
- The flow can be used to
  - Post the teams message
  - Assign to a team member
  - whatever we like
- Tidy attachments to a folder rather than the root of the team (when added via the planner app)

# Benefits
- The whole team can see the status of the shipments to send for the week
  - Planner Web. in the scheduled view
  - Planner Web. in the percent complete view
  - Plaaner app on mobile
  - Add the plan to teams
  - Get teams notification on phone when #an order changes~ state!
  - No need to instal another app on phone
  - Managers can monitor shipments

[![Hack Together: Microsoft Graph and .NET](https://img.shields.io/badge/Microsoft%20-Hack--Together-orange?style=for-the-badge&logo=microsoft)](https://github.com/microsoft/hack-together)
