# Music.Api

Experiments with Akka.net; event sourcing and CQRS

## User story

- User is able to submit albums, tracks and publish them after paying for release.
- User is able to modify his albums, tracks, metadatas and so on.
- User is able to see published albums.

## Technical side

- System is built according to event sourcing and CQRS principles
- All the interactions with albums, metadata and tracks are being implemented using actors
- For event storing GCP Datastore is used.

## What is done

- Architecture and solution structure is designed
- Actor system is designed and implemented

## To be done

- Finish integration with GCP
- Implement security and permissions
- Redesign use of create and update events
