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
    //   blogId: 'Base64Guid',
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
    //   defaultWeeklyReleaseTime: 0
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
    //   name: '', /* optional */
    //   username: '',
    //   email: '',
    //   profileImage: { /* optional */
    //     fileId: 'Base64Guid',
    //     containerName: ''
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

    // userId = 'Base64Guid'
    // creatorInformation = {
    //   name: ''
    // }
    service.putCreatorInformation = function(userId, creatorInformation) {
      return $http.put(apiBaseUri + 'accountSettings/' + encodeURIComponent(userId) + '/creatorInformation', creatorInformation).catch(function(response) {
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
    //   password: '',
    //   creatorName: '' /* optional */
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
      return $http.get(apiBaseUri + 'membership/passwordResetTokens/' + encodeURIComponent(userId) + '?' + (token === undefined ? '' : 'token=' + encodeURIComponent(token) + '&')).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // registerInterestData = {
    //   name: '',
    //   email: ''
    // }
    service.postRegisteredInterest = function(registerInterestData) {
      return $http.post(apiBaseUri + 'membership/registeredInterest', registerInterestData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // identifiedUserData = {
    //   isUpdate: false,
    //   name: '',
    //   username: '',
    //   email: ''
    // }
    service.postIdentifiedUser = function(identifiedUserData) {
      return $http.post(apiBaseUri + 'membership/identifiedUsers', identifiedUserData).catch(function(response) {
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
    //     file: { /* optional */
    //       fileId: 'Base64Guid',
    //       containerName: ''
    //     },
    //     fileSource: { /* optional */
    //       fileName: '',
    //       fileExtension: '',
    //       contentType: '',
    //       size: 0,
    //       renderSize: { /* optional */
    //         width: 0,
    //         height: 0
    //       }
    //     },
    //     image: { /* optional */
    //       fileId: 'Base64Guid',
    //       containerName: ''
    //     },
    //     imageSource: { /* optional */
    //       fileName: '',
    //       fileExtension: '',
    //       contentType: '',
    //       size: 0,
    //       renderSize: { /* optional */
    //         width: 0,
    //         height: 0
    //       }
    //     },
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
    // newsfeedPaginationData = {
    //   startIndex: 0,
    //   count: 0
    // }
    // result = [
    //   {
    //     postId: 'Base64Guid',
    //     channelId: 'Base64Guid',
    //     collectionId: 'Base64Guid', /* optional */
    //     comment: '', /* optional */
    //     file: { /* optional */
    //       fileId: 'Base64Guid',
    //       containerName: ''
    //     },
    //     fileSource: { /* optional */
    //       fileName: '',
    //       fileExtension: '',
    //       contentType: '',
    //       size: 0,
    //       renderSize: { /* optional */
    //         width: 0,
    //         height: 0
    //       }
    //     },
    //     image: { /* optional */
    //       fileId: 'Base64Guid',
    //       containerName: ''
    //     },
    //     imageSource: { /* optional */
    //       fileName: '',
    //       fileExtension: '',
    //       contentType: '',
    //       size: 0,
    //       renderSize: { /* optional */
    //         width: 0,
    //         height: 0
    //       }
    //     },
    //     liveDate: '2015-12-25T14:45:05Z'
    //   }
    // ]
    service.getCreatorNewsfeed = function(creatorId, newsfeedPaginationData) {
      return $http.get(apiBaseUri + 'posts/creatorNewsfeed/' + encodeURIComponent(creatorId) + '?' + (newsfeedPaginationData.startIndex === undefined ? '' : 'startIndex=' + encodeURIComponent(newsfeedPaginationData.startIndex) + '&') + (newsfeedPaginationData.count === undefined ? '' : 'count=' + encodeURIComponent(newsfeedPaginationData.count) + '&')).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // filter = {
    //   creatorId: 'Base64Guid', /* optional */
    //   channelId: 'Base64Guid', /* optional */
    //   collectionId: 'Base64Guid', /* optional */
    //   origin: '2015-12-25T14:45:05Z', /* optional */
    //   searchForwards: false,
    //   startIndex: 0,
    //   count: 0
    // }
    // result = {
    //   posts: [
    //     {
    //       creatorId: 'Base64Guid',
    //       postId: 'Base64Guid',
    //       blogId: 'Base64Guid',
    //       channelId: 'Base64Guid',
    //       collectionId: 'Base64Guid', /* optional */
    //       comment: '', /* optional */
    //       file: { /* optional */
    //         fileId: 'Base64Guid',
    //         containerName: ''
    //       },
    //       fileSource: { /* optional */
    //         fileName: '',
    //         fileExtension: '',
    //         contentType: '',
    //         size: 0,
    //         renderSize: { /* optional */
    //           width: 0,
    //           height: 0
    //         }
    //       },
    //       image: { /* optional */
    //         fileId: 'Base64Guid',
    //         containerName: ''
    //       },
    //       imageSource: { /* optional */
    //         fileName: '',
    //         fileExtension: '',
    //         contentType: '',
    //         size: 0,
    //         renderSize: { /* optional */
    //           width: 0,
    //           height: 0
    //         }
    //       },
    //       liveDate: '2015-12-25T14:45:05Z'
    //     }
    //   ]
    // }
    service.getNewsfeed = function(filter) {
      return $http.get(apiBaseUri + 'posts/newsfeed?' + (filter.creatorId === undefined ? '' : 'creatorId=' + encodeURIComponent(filter.creatorId) + '&') + (filter.channelId === undefined ? '' : 'channelId=' + encodeURIComponent(filter.channelId) + '&') + (filter.collectionId === undefined ? '' : 'collectionId=' + encodeURIComponent(filter.collectionId) + '&') + (filter.origin === undefined ? '' : 'origin=' + encodeURIComponent(filter.origin) + '&') + (filter.searchForwards === undefined ? '' : 'searchForwards=' + encodeURIComponent(filter.searchForwards) + '&') + (filter.startIndex === undefined ? '' : 'startIndex=' + encodeURIComponent(filter.startIndex) + '&') + (filter.count === undefined ? '' : 'count=' + encodeURIComponent(filter.count) + '&')).catch(function(response) {
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
      return $http.post(apiBaseUri + 'posts/queued', JSON.stringify(postId)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // postId = 'Base64Guid'
    service.postToLive = function(postId) {
      return $http.post(apiBaseUri + 'posts/live', JSON.stringify(postId)).catch(function(response) {
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

angular.module('webApp').factory('blogAccessStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    // blogId = 'Base64Guid'
    // data = {
    //   emails: [
    //     ''
    //   ]
    // }
    // result = {
    //   invalidEmailAddresses: [
    //     ''
    //   ]
    // }
    service.putFreeAccessList = function(blogId, data) {
      return $http.put(apiBaseUri + 'blogAccess/freeAccessList/' + encodeURIComponent(blogId), data).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // blogId = 'Base64Guid'
    // result = {
    //   freeAccessUsers: [
    //     {
    //       email: '',
    //       userId: 'Base64Guid', /* optional */
    //       username: '', /* optional */
    //       channelIds: [
    //         'Base64Guid'
    //       ]
    //     }
    //   ]
    // }
    service.getFreeAccessList = function(blogId) {
      return $http.get(apiBaseUri + 'blogAccess/freeAccessList/' + encodeURIComponent(blogId)).catch(function(response) {
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

    // blogId = 'Base64Guid'
    // subscriptionData = {
    //   subscriptions: [
    //     {
    //       channelId: 'Base64Guid',
    //       acceptedPrice: 0
    //     }
    //   ]
    // }
    service.putBlogSubscriptions = function(blogId, subscriptionData) {
      return $http.put(apiBaseUri + 'subscriptions/blogs/' + encodeURIComponent(blogId), subscriptionData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // channelId = 'Base64Guid'
    service.deleteChannelSubscription = function(channelId) {
      return $http.delete(apiBaseUri + 'subscriptions/channels/' + encodeURIComponent(channelId)).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // channelId = 'Base64Guid'
    // subscriptionData = {
    //   acceptedPrice: 0
    // }
    service.putChannelSubscription = function(channelId, subscriptionData) {
      return $http.put(apiBaseUri + 'subscriptions/channels/' + encodeURIComponent(channelId), subscriptionData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

angular.module('webApp').factory('blogStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    // blogData = {
    //   name: '',
    //   tagline: '',
    //   basePrice: 0
    // }
    // result = 'Base64Guid'
    service.postBlog = function(blogData) {
      return $http.post(apiBaseUri + 'blogs', blogData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // blogId = 'Base64Guid'
    // blogData = {
    //   name: '',
    //   tagline: '',
    //   introduction: '',
    //   headerImageFileId: 'Base64Guid', /* optional */
    //   video: '', /* optional */
    //   description: '' /* optional */
    // }
    service.putBlog = function(blogId, blogData) {
      return $http.put(apiBaseUri + 'blogs/' + encodeURIComponent(blogId), blogData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    // username = ''
    // result = {
    //   userId: 'Base64Guid',
    //   profileImage: { /* optional */
    //     fileId: 'Base64Guid',
    //     containerName: ''
    //   },
    //   blog: {
    //     blogId: 'Base64Guid',
    //     blogName: '',
    //     name: '',
    //     tagline: '',
    //     introduction: '',
    //     creationDate: '2015-12-25T14:45:05Z',
    //     headerImage: { /* optional */
    //       fileId: 'Base64Guid',
    //       containerName: ''
    //     },
    //     video: '', /* optional */
    //     description: '', /* optional */
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
    service.getLandingPage = function(username) {
      return $http.get(apiBaseUri + 'blogs/landingPages/' + encodeURIComponent(username)).catch(function(response) {
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
    //     blogId: 'Base64Guid', /* optional */
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
    //   },
    //   accountSettings: { /* optional */
    //     name: '', /* optional */
    //     username: '',
    //     email: '',
    //     profileImage: { /* optional */
    //       fileId: 'Base64Guid',
    //       containerName: ''
    //     }
    //   },
    //   blog: { /* optional */
    //     blogId: 'Base64Guid',
    //     blogName: '',
    //     name: '',
    //     tagline: '',
    //     introduction: '',
    //     creationDate: '2015-12-25T14:45:05Z',
    //     headerImage: { /* optional */
    //       fileId: 'Base64Guid',
    //       containerName: ''
    //     },
    //     video: '', /* optional */
    //     description: '', /* optional */
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
    //   },
    //   subscriptions: { /* optional */
    //     blogs: [
    //       {
    //         blogId: 'Base64Guid',
    //         name: '',
    //         creatorId: 'Base64Guid',
    //         username: '',
    //         profileImage: { /* optional */
    //           fileId: 'Base64Guid',
    //           containerName: ''
    //         },
    //         freeAccess: false,
    //         channels: [
    //           {
    //             channelId: 'Base64Guid',
    //             name: '',
    //             acceptedPrice: 0,
    //             priceInUsCentsPerWeek: 0,
    //             isDefault: false,
    //             priceLastSetDate: '2015-12-25T14:45:05Z',
    //             subscriptionStartDate: '2015-12-25T14:45:05Z',
    //             isVisibleToNonSubscribers: false,
    //             collections: [
    //               {
    //                 collectionId: 'Base64Guid',
    //                 name: ''
    //               }
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
    //     blogId: 'Base64Guid', /* optional */
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
    //   },
    //   accountSettings: { /* optional */
    //     name: '', /* optional */
    //     username: '',
    //     email: '',
    //     profileImage: { /* optional */
    //       fileId: 'Base64Guid',
    //       containerName: ''
    //     }
    //   },
    //   blog: { /* optional */
    //     blogId: 'Base64Guid',
    //     blogName: '',
    //     name: '',
    //     tagline: '',
    //     introduction: '',
    //     creationDate: '2015-12-25T14:45:05Z',
    //     headerImage: { /* optional */
    //       fileId: 'Base64Guid',
    //       containerName: ''
    //     },
    //     video: '', /* optional */
    //     description: '', /* optional */
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
    //   },
    //   subscriptions: { /* optional */
    //     blogs: [
    //       {
    //         blogId: 'Base64Guid',
    //         name: '',
    //         creatorId: 'Base64Guid',
    //         username: '',
    //         profileImage: { /* optional */
    //           fileId: 'Base64Guid',
    //           containerName: ''
    //         },
    //         freeAccess: false,
    //         channels: [
    //           {
    //             channelId: 'Base64Guid',
    //             name: '',
    //             acceptedPrice: 0,
    //             priceInUsCentsPerWeek: 0,
    //             isDefault: false,
    //             priceLastSetDate: '2015-12-25T14:45:05Z',
    //             subscriptionStartDate: '2015-12-25T14:45:05Z',
    //             isVisibleToNonSubscribers: false,
    //             collections: [
    //               {
    //                 collectionId: 'Base64Guid',
    //                 name: ''
    //               }
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

