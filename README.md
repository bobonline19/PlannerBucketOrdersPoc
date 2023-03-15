# PlannerBucketOrdersPoc
A proof of concept using a planner board to track order fulfillment by moving tasks through buckets
<picture>
  <img alt="The planner that is monitored by the code" src="https://github.com/bobonline19/PlannerBucketOrdersPoc/blob/798a9cca639ea6aff7fd5c2ea789331b7de0cb00/docs/Planner.jpeg">
</picture>

Currently it can be run as a scheduled task on a computer to post a message to the Teams channel when a task changes bucket.
<picture>
  <img alt="A teams message because a task has changed bucket" src="https://github.com/bobonline19/PlannerBucketOrdersPoc/blob/798a9cca639ea6aff7fd5c2ea789331b7de0cb00/docs/change%20notification.png">
</picture>


Lets see how far we get with this in very little time.

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
 
 

[![Hack Together: Microsoft Graph and .NET](https://img.shields.io/badge/Microsoft%20-Hack--Together-orange?style=for-the-badge&logo=microsoft)](https://github.com/microsoft/hack-together)
