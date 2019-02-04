# Offline Data Sync Overview

The service will initially be an online-first method to persist app data in the cloud built on top of CosmosDB. Our core functionality will include:

- The ability to provision or connect to a CosmosDB instance to store app data with an Azure subscription (Core SQL protocol only)
- The ability to perform CRUD operations on app data (documents) with our native SDKs
    - A Document can either be Public (and read only) or Private (and read-write by a single authenticated user)
- The ability to perform simple queries on stored data using the SDKs
- An Management UI within the App Center portal, enabling CRUD access to app data (documents)

Additional features we plan on implementing following the initial release:

- Offline data persistence
- Conflict Resolution
- Advanced Queries

For security, we're implementing a token exchange service. This will allow an exchange of an app secret plus an optional AAD B2C token for a CosmosDB resource token. This service is responsible for validating app secrets, validating AAD B2C tokens, creating CosmosDB users (for each AAD B2C user), associating default permissions for each CosmosDB user, and caching and returning resource tokens. This will eventually be tightly coupled with our [Identity Service](https://github.com/Microsoft/appcenter/pull/16) in order to give better access control, permissions, and other identity functionality in correlation with Data Sync. 

We have decided to prioritize the following platforms:

- Android
- iOS

Additional platforms that we will target in the future:

- Xamarin
- React Native
- .NET Core
- JavaScript (browser)
- macOS
- Universal Windows Platform (UWP)

## MVP User Scenarios

#### 1. As a developer, I can provision a CosmosDB instance for storing my app data

    - I can link to an existing Azure subscription and create a new CosmosDB resource.
    - I can change the default name of the collection when setting it up for the first time

#### 2. As a developer, I can connect or disconnect my app to an existing Cosmos database for data storage

    - I can connect to an existing Azure subscription and my CosmosDB resource. Please note that you will only be able to connect if the CosmosDB resource has the Core(SQL) api.

#### 3. As a developer, I can specify my CosmosDB account name, location of my resource, RU (throughput) when setting up a CosmosDB resource in the App Center Portal

    - Directly from the App Center portal, i'm able to configure my CosmosDB instance

#### 4. As a developer, I can easily navigate to the Azure CosmosDB resource from the App Center portal to view and edit my app data

#### 5.As a developer, I can perform CRUD operations on my app data using the App Center Data Sync SDK and the App Center Portal

    - I can create, get/read, update, and delete documents
    - I can delete an entire collection and all the documents in it

#### 6. As a developer, I can view my app data in the App Center portal

    - I can browse through collections and documents
    - I can filter documents by user identifier using AAD B2C

#### 7. As a developer, I can upload JSON files to the App Center portal in order to add them to my collection

    - I can upload a single json document or an array of documents
    - I can pick whether this document is public or private

#### 8. As a developer, I can query and display my app data in the portal

    - I can perform simple queries to filter data and display it

## SDK Signatures

```java
package com.microsoft.appcenter.datasync;

// App Center DataSync module
public class DataSync {

  // Default permissions:
  // Get a public partition
  public static ReadOnlyPartition getReadOnlyPartition(String name);

  // Default permissions:
  // Get a user partition
  public static Partition getUserPartition(String name);

  // Get any partition
  // Developers may use this signature if they created permissions themselves
  // in CosmosDB
  public static Partition getPartition(String name);

  // TODO: add a method to get the resource token (+ information indicating if it is for a signed in user or anonymous user)

  // After Build
  // public static void setNetworkEnabled(boolean networkEnabled);
  // public static boolean getNetworkEnabled();
  // public static void setPersistenceEnabled(boolean persistenceEnabled);
  // public static boolean getPersistenceEnabled();
}

// Public read-only partition
public interface ReadOnlyPartition {

  // Read
  <T> AppCenterFuture<Document<T>> read(String documentId, class<T> documentType);

  // List (need optional signature to configure page size)
  <T> AppCenterFuture<Documents<T>> list(class<T> documentType);

}

// User read-write partition
public interface Partition extends Partition {

  // Create a document
  <T> AppCenterFuture<Document<T>> create(String documentId, T document);

  // Replace a document
  <T> AppCenterFuture<Document<T>> replace(String documentId, T document);

  // Create or replace (upsert) a document
  <T> AppCenterFuture<Document<T>> createOrReplace(String documentId, T document);

  // Delete a document
  AppCenterFuture<Document<Void>> delete(String documentId);

}

public interface Document<T> {

  // Non-serialized data (or null)
  public String getJsonDocument();

  // Deserialized data (or null)
  public T getDocument();

  // Error or null
  public DataSyncError getError();

  // ID + document metadata
  public String getId();
  public String getEtag();
  public Timestamp getTimestamp();

  // When caching is supported:
  // Flag indicating if data was retrieved from the local cache (for offline mode)
  // public boolean isFromCache();

}

public interface Documents<T> {

  // List of results
  public List<Document<T>> getDocuments();

  // List of results (direct)
  public List<T> asList();

  // Flag indicating if more results can be fetched
  public boolean hasNext();

  // Fetch more results
  public AppCenterFuture<Documents<T>> next();

}

public interface DataSyncError {
  // TO DEFINE LATER: connectivity, conflicts, deserialization errors, unauthorized access
}
```

# Mockups

1. Connect and provision services **Work in Progress**
2. View utilization metrics **Work in Progress**
3. Browse document collections **Work in Progress**
