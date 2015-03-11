angular.module('webApp').factory('availabilityStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    // result = {
    //   database: false,
    //   api: false
    // }
    service.get = function() {
      return $http.get(apiBaseUri + 'availability').catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.head = function() {
      return $http.get(apiBaseUri + 'availability').catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

angular.module('webApp').factory('channelStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    // newChannelData = {
    //   subscriptionId: 'Base64Guid',
    //   name: '',
    //   description: '',
    //   price: 0,
    //   isVisibleToNonSubscribers: false
    // }
    // result = 'Base64Guid'
    service.postChannel = function(newChannelData) {
      return $http.post(apiBaseUri + 'channels', newChannelData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // channelId = 'Base64Guid'
    // channelData = {
    //   name: '',
    //   description: '',
    //   price: 0,
    //   isVisibleToNonSubscribers: false
    // }
    service.putChannel = function(channelId, channelData) {
      return $http.put(apiBaseUri + 'channels/' + encodeURIComponent(channelId), channelData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // channelId = 'Base64Guid'
    service.deleteChannel = function(channelId) {
      return $http.delete(apiBaseUri + 'channels/' + encodeURIComponent(channelId)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

angular.module('webApp').factory('collectionStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    // newCollectionData = {
    //   channelId: 'Base64Guid',
    //   name: ''
    // }
    // result = {
    //   collectionId: 'Base64Guid',
    //   defaultWeeklyReleaseTime: {
    //     value: 0
    //   }
    // }
    service.postCollection = function(newCollectionData) {
      return $http.post(apiBaseUri + 'collections', newCollectionData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // collectionId = 'Base64Guid'
    // collectionData = {
    //   channelId: 'Base64Guid',
    //   name: '',
    //   weeklyReleaseSchedule: [
    //     0
    //   ]
    // }
    service.putCollection = function(collectionId, collectionData) {
      return $http.put(apiBaseUri + 'collections/' + encodeURIComponent(collectionId), collectionData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // collectionId = 'Base64Guid'
    service.deleteCollection = function(collectionId) {
      return $http.delete(apiBaseUri + 'collections/' + encodeURIComponent(collectionId)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // collectionId = 'Base64Guid'
    // result = '2015-12-25T14:45:05Z'
    service.getLiveDateOfNewQueuedPost = function(collectionId) {
      return $http.get(apiBaseUri + 'collections/' + encodeURIComponent(collectionId) + '/newQueuedPostLiveDate').catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

angular.module('webApp').factory('endToEndTestInboxStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    // mailboxName = ''
    service.getLatestMessageAndClearMailbox = function(mailboxName) {
      return $http.get(apiBaseUri + 'testMailboxes/' + encodeURIComponent(mailboxName)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

angular.module('webApp').factory('accountSettingsStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    // userId = 'Base64Guid'
    // result = {
    //   email: {
    //     value: ''
    //   },
    //   profileImage: { /* optional */
    //     fileId: 'Base64Guid',
    //     containerName: '',
    //     blobName: '',
    //     uri: ''
    //   }
    // }
    service.get = function(userId) {
      return $http.get(apiBaseUri + 'accountSettings/' + encodeURIComponent(userId)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // userId = 'Base64Guid'
    // updatedAccountSettingsData = {
    //   newUsername: '',
    //   newEmail: '',
    //   newPassword: '', /* optional */
    //   newProfileImageId: 'Base64Guid' /* optional */
    // }
    service.put = function(userId, updatedAccountSettingsData) {
      return $http.put(apiBaseUri + 'accountSettings/' + encodeURIComponent(userId), updatedAccountSettingsData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

angular.module('webApp').factory('membershipStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    // registrationData = {
    //   exampleWork: '', /* optional */
    //   email: '',
    //   username: '',
    //   password: ''
    // }
    service.postRegistration = function(registrationData) {
      return $http.post(apiBaseUri + 'membership/registrations', registrationData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // username = ''
    service.getUsernameAvailability = function(username) {
      return $http.get(apiBaseUri + 'membership/availableUsernames/' + encodeURIComponent(username)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // passwordResetRequestData = {
    //   email: '', /* optional */
    //   username: '' /* optional */
    // }
    service.postPasswordResetRequest = function(passwordResetRequestData) {
      return $http.post(apiBaseUri + 'membership/passwordResetRequests', passwordResetRequestData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // passwordResetConfirmationData = {
    //   userId: 'Base64Guid',
    //   newPassword: '',
    //   token: ''
    // }
    service.postPasswordResetConfirmation = function(passwordResetConfirmationData) {
      return $http.post(apiBaseUri + 'membership/passwordResetConfirmations', passwordResetConfirmationData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // userId = 'Base64Guid'
    // token = ''
    service.getPasswordResetTokenValidity = function(userId, token) {
      return $http.get(apiBaseUri + 'membership/passwordResetTokens/' + encodeURIComponent(userId) + '?token=' + encodeURIComponent(token)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

angular.module('webApp').factory('logStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    // logMessage = {
    //   level: '',
    //   payload: { arbitrary: 'json' }
    // }
    service.post = function(logMessage) {
      return $http.post(apiBaseUri + 'log', logMessage).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

angular.module('webApp').factory('postsStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    // creatorId = 'Base64Guid'
    // result = [
    //   {
    //     postId: 'Base64Guid',
    //     channelId: 'Base64Guid',
    //     collectionId: 'Base64Guid', /* optional */
    //     comment: '', /* optional */
    //     fileId: 'Base64Guid', /* optional */
    //     imageId: 'Base64Guid', /* optional */
    //     scheduledByQueue: false,
    //     liveDate: '2015-12-25T14:45:05Z'
    //   }
    // ]
    service.getCreatorBacklog = function(creatorId) {
      return $http.get(apiBaseUri + 'posts/creatorBacklog/' + encodeURIComponent(creatorId)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // creatorId = 'Base64Guid'
    // startIndex = 0
    // count = 0
    // result = [
    //   {
    //     postId: 'Base64Guid',
    //     channelId: 'Base64Guid',
    //     collectionId: 'Base64Guid', /* optional */
    //     comment: '', /* optional */
    //     fileId: 'Base64Guid', /* optional */
    //     imageId: 'Base64Guid', /* optional */
    //     liveDate: '2015-12-25T14:45:05Z'
    //   }
    // ]
    service.getCreatorNewsfeed = function(creatorId, startIndex, count) {
      return $http.get(apiBaseUri + 'posts/creatorNewsfeed/' + encodeURIComponent(creatorId) + '?startIndex=' + encodeURIComponent(startIndex) + '&count=' + encodeURIComponent(count)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // postId = 'Base64Guid'
    service.deletePost = function(postId) {
      return $http.delete(apiBaseUri + 'posts/' + encodeURIComponent(postId)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // collectionId = 'Base64Guid'
    // newQueueOrder = [
    //   'Base64Guid'
    // ]
    service.postNewQueueOrder = function(collectionId, newQueueOrder) {
      return $http.post(apiBaseUri + 'posts/queues/' + encodeURIComponent(collectionId), newQueueOrder).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // postId = 'Base64Guid'
    service.postToQueue = function(postId) {
      return $http.post(apiBaseUri + 'posts/queued', postId).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // postId = 'Base64Guid'
    service.postToLive = function(postId) {
      return $http.post(apiBaseUri + 'posts/live', postId).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // postId = 'Base64Guid'
    // newLiveDate = '2015-12-25T14:45:05Z'
    service.putLiveDate = function(postId, newLiveDate) {
      return $http.put(apiBaseUri + 'posts/' + encodeURIComponent(postId) + '/liveDate', newLiveDate).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // noteData = {
    //   channelId: 'Base64Guid',
    //   note: '',
    //   scheduledPostTime: '2015-12-25T14:45:05Z' /* optional */
    // }
    service.postNote = function(noteData) {
      return $http.post(apiBaseUri + 'posts/notes', noteData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // postId = 'Base64Guid'
    // noteData = {
    //   channelId: 'Base64Guid',
    //   note: ''
    // }
    service.putNote = function(postId, noteData) {
      return $http.put(apiBaseUri + 'posts/notes/' + encodeURIComponent(postId), noteData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // imageData = {
    //   collectionId: 'Base64Guid',
    //   fileId: 'Base64Guid',
    //   comment: '', /* optional */
    //   scheduledPostTime: '2015-12-25T14:45:05Z', /* optional */
    //   isQueued: false
    // }
    service.postImage = function(imageData) {
      return $http.post(apiBaseUri + 'posts/images', imageData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // postId = 'Base64Guid'
    // imageData = {
    //   imageFileId: 'Base64Guid',
    //   comment: '' /* optional */
    // }
    service.putImage = function(postId, imageData) {
      return $http.put(apiBaseUri + 'posts/images/' + encodeURIComponent(postId), imageData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // fileData = {
    //   collectionId: 'Base64Guid',
    //   fileId: 'Base64Guid',
    //   comment: '', /* optional */
    //   scheduledPostTime: '2015-12-25T14:45:05Z', /* optional */
    //   isQueued: false
    // }
    service.postFile = function(fileData) {
      return $http.post(apiBaseUri + 'posts/files', fileData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // postId = 'Base64Guid'
    // fileData = {
    //   fileId: 'Base64Guid',
    //   comment: '' /* optional */
    // }
    service.putFile = function(postId, fileData) {
      return $http.put(apiBaseUri + 'posts/files/' + encodeURIComponent(postId), fileData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

angular.module('webApp').factory('subscriptionStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    // subscriptionData = {
    //   subscriptionName: '',
    //   tagline: '',
    //   basePrice: 0
    // }
    // result = 'Base64Guid'
    service.postSubscription = function(subscriptionData) {
      return $http.post(apiBaseUri + 'subscriptions', subscriptionData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // subscriptionId = 'Base64Guid'
    // subscriptionData = {
    //   subscriptionName: '',
    //   tagline: '',
    //   introduction: '',
    //   headerImageFileId: 'Base64Guid', /* optional */
    //   video: '', /* optional */
    //   description: '' /* optional */
    // }
    service.putSubscription = function(subscriptionId, subscriptionData) {
      return $http.put(apiBaseUri + 'subscriptions/' + encodeURIComponent(subscriptionId), subscriptionData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // subscriptionId = 'Base64Guid'
    // result = {
    //   subscriptionId: 'Base64Guid',
    //   creatorId: 'Base64Guid',
    //   subscriptionName: '',
    //   tagline: '',
    //   introduction: '',
    //   creationDate: '2015-12-25T14:45:05Z',
    //   headerImage: { /* optional */
    //     fileId: 'Base64Guid',
    //     containerName: '',
    //     blobName: '',
    //     uri: ''
    //   },
    //   video: '', /* optional */
    //   description: '' /* optional */
    // }
    service.getSubscription = function(subscriptionId) {
      return $http.get(apiBaseUri + 'subscriptions/' + encodeURIComponent(subscriptionId)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

angular.module('webApp').factory('userAccessSignaturesStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    // result = {
    //   timeToLiveSeconds: 0,
    //   publicSignature: {
    //     containerName: '',
    //     uri: '',
    //     signature: '',
    //     expiry: '2015-12-25T14:45:05Z'
    //   },
    //   privateSignatures: [
    //     {
    //       creatorId: 'Base64Guid',
    //       information: {
    //         containerName: '',
    //         uri: '',
    //         signature: '',
    //         expiry: '2015-12-25T14:45:05Z'
    //       }
    //     }
    //   ]
    // }
    service.getForVisitor = function() {
      return $http.get(apiBaseUri + 'userAccessSignatures').catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // userId = 'Base64Guid'
    // result = {
    //   timeToLiveSeconds: 0,
    //   publicSignature: {
    //     containerName: '',
    //     uri: '',
    //     signature: '',
    //     expiry: '2015-12-25T14:45:05Z'
    //   },
    //   privateSignatures: [
    //     {
    //       creatorId: 'Base64Guid',
    //       information: {
    //         containerName: '',
    //         uri: '',
    //         signature: '',
    //         expiry: '2015-12-25T14:45:05Z'
    //       }
    //     }
    //   ]
    // }
    service.getForUser = function(userId) {
      return $http.get(apiBaseUri + 'userAccessSignatures/' + encodeURIComponent(userId)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

angular.module('webApp').factory('fileUploadStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    // data = {
    //   filePath: '',
    //   purpose: ''
    // }
    // result = {
    //   fileId: 'Base64Guid',
    //   accessInformation: {
    //     containerName: '',
    //     blobName: '',
    //     uri: '',
    //     signature: '',
    //     expiry: '2015-12-25T14:45:05Z'
    //   }
    // }
    service.postUploadRequest = function(data) {
      return $http.post(apiBaseUri + 'files/uploadRequests', data).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // fileId = 'Base64Guid'
    service.postUploadCompleteNotification = function(fileId) {
      return $http.post(apiBaseUri + 'files/uploadCompleteNotifications/' + encodeURIComponent(fileId)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

angular.module('webApp').factory('userStateStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    // userId = 'Base64Guid'
    // result = {
    //   accessSignatures: {
    //     timeToLiveSeconds: 0,
    //     publicSignature: {
    //       containerName: '',
    //       uri: '',
    //       signature: '',
    //       expiry: '2015-12-25T14:45:05Z'
    //     },
    //     privateSignatures: [
    //       {
    //         creatorId: 'Base64Guid',
    //         information: {
    //           containerName: '',
    //           uri: '',
    //           signature: '',
    //           expiry: '2015-12-25T14:45:05Z'
    //         }
    //       }
    //     ]
    //   },
    //   creatorStatus: { /* optional */
    //     subscriptionId: 'Base64Guid', /* optional */
    //     mustWriteFirstPost: false
    //   },
    //   createdChannelsAndCollections: { /* optional */
    //     channels: [
    //       {
    //         channelId: 'Base64Guid',
    //         name: '',
    //         description: '',
    //         priceInUsCentsPerWeek: 0,
    //         isDefault: false,
    //         isVisibleToNonSubscribers: false,
    //         collections: [
    //           {
    //             collectionId: 'Base64Guid',
    //             name: '',
    //             weeklyReleaseSchedule: [
    //               0
    //             ]
    //           }
    //         ]
    //       }
    //     ]
    //   }
    // }
    service.getUserState = function(userId) {
      return $http.get(apiBaseUri + 'userState/' + encodeURIComponent(userId)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // result = {
    //   accessSignatures: {
    //     timeToLiveSeconds: 0,
    //     publicSignature: {
    //       containerName: '',
    //       uri: '',
    //       signature: '',
    //       expiry: '2015-12-25T14:45:05Z'
    //     },
    //     privateSignatures: [
    //       {
    //         creatorId: 'Base64Guid',
    //         information: {
    //           containerName: '',
    //           uri: '',
    //           signature: '',
    //           expiry: '2015-12-25T14:45:05Z'
    //         }
    //       }
    //     ]
    //   },
    //   creatorStatus: { /* optional */
    //     subscriptionId: 'Base64Guid', /* optional */
    //     mustWriteFirstPost: false
    //   },
    //   createdChannelsAndCollections: { /* optional */
    //     channels: [
    //       {
    //         channelId: 'Base64Guid',
    //         name: '',
    //         description: '',
    //         priceInUsCentsPerWeek: 0,
    //         isDefault: false,
    //         isVisibleToNonSubscribers: false,
    //         collections: [
    //           {
    //             collectionId: 'Base64Guid',
    //             name: '',
    //             weeklyReleaseSchedule: [
    //               0
    //             ]
    //           }
    //         ]
    //       }
    //     ]
    //   }
    // }
    service.getVisitorState = function() {
      return $http.get(apiBaseUri + 'userState').catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

