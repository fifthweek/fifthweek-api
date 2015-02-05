//
// Availability API stub.
//
angular.module('webApp').factory('availabilityStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

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

//
// Channel API stub.
//
angular.module('webApp').factory('channelStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    service.postChannel = function(newChannelData) {
      return $http.post(apiBaseUri + 'collections', newChannelData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.putChannel = function(channelId, channelData) {
      return $http.put(apiBaseUri + 'collections/' + channelId, channelData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.deleteChannel = function(channelId) {
      return $http.delete(apiBaseUri + 'collections/' + channelId).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

//
// Collection API stub.
//
angular.module('webApp').factory('collectionStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    service.postCollection = function(newCollectionData) {
      return $http.post(apiBaseUri + 'collections', newCollectionData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.putCollection = function(collectionId, collectionData) {
      return $http.put(apiBaseUri + 'collections/' + collectionId, collectionData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.deleteCollection = function(collectionId) {
      return $http.delete(apiBaseUri + 'collections/' + collectionId).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.getLiveDateOfNewQueuedPost = function(collectionId) {
      return $http.get(apiBaseUri + 'collections/' + collectionId + '/newQueuedPostLiveDate').catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

//
// Membership API stub.
//
angular.module('webApp').factory('membershipStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    service.postRegistration = function(registrationData) {
      return $http.post(apiBaseUri + 'membership/registrations', registrationData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.getUsernameAvailability = function(username) {
      return $http.get(apiBaseUri + 'membership/availableUsernames/' + username).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.postPasswordResetRequest = function(passwordResetRequestData) {
      return $http.post(apiBaseUri + 'membership/passwordResetRequests', passwordResetRequestData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.postPasswordResetConfirmation = function(passwordResetConfirmationData) {
      return $http.post(apiBaseUri + 'membership/passwordResetConfirmations', passwordResetConfirmationData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.getPasswordResetTokenValidity = function(userId, token) {
      return $http.get(apiBaseUri + 'membership/passwordResetTokens/' + userId + '/' + token).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

//
// Log API stub.
//
angular.module('webApp').factory('logStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    service.post = function(logMessage) {
      return $http.post(apiBaseUri + 'log', logMessage).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

//
// File post API stub.
//
angular.module('webApp').factory('filePostStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    service.postFile = function(fileData) {
      return $http.post(apiBaseUri + 'posts/files', fileData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.putFile = function(postId, fileData) {
      return $http.put(apiBaseUri + 'posts/files/' + postId, fileData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

//
// Note post API stub.
//
angular.module('webApp').factory('notePostStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    service.postNote = function(noteData) {
      return $http.post(apiBaseUri + 'posts/notes', noteData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.putNote = function(postId, noteData) {
      return $http.put(apiBaseUri + 'posts/notes/' + postId, noteData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

//
// Image post API stub.
//
angular.module('webApp').factory('imagePostStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    service.postImage = function(imageData) {
      return $http.post(apiBaseUri + 'posts/images', imageData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.putImage = function(postId, imageData) {
      return $http.put(apiBaseUri + 'posts/images/' + postId, imageData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

//
// Post API stub.
//
angular.module('webApp').factory('postStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    service.getCreatorBacklog = function(creatorId) {
      return $http.get(apiBaseUri + 'posts/creatorBacklog/' + creatorId).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.getCreatorNewsfeed = function(creatorId, startIndex, count) {
      return $http.get(apiBaseUri + 'posts/creatorNewsfeed/' + creatorId + '?startIndex=' + startIndex + '&count=' + count).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.deletePost = function(postId) {
      return $http.delete(apiBaseUri + 'posts/' + postId).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.postNewQueueOrder = function(collectionId, newQueueOrder) {
      return $http.post(apiBaseUri + 'posts/queues/' + collectionId, newQueueOrder).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.postToQueue = function(postId) {
      return $http.post(apiBaseUri + 'posts/queued', postId).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.postToLive = function(postId) {
      return $http.post(apiBaseUri + 'posts/live', postId).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.putLiveDate = function(postId, newLiveDate) {
      return $http.put(apiBaseUri + 'posts/' + postId + '/liveDate', newLiveDate).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

//
// Subscription API stub.
//
angular.module('webApp').factory('subscriptionStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    service.postSubscription = function(subscriptionData) {
      return $http.post(apiBaseUri + 'subscriptions', subscriptionData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.putSubscription = function(subscriptionId, subscriptionData) {
      return $http.put(apiBaseUri + 'subscriptions/' + subscriptionId, subscriptionData).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

//
// User access signatures API stub.
//
angular.module('webApp').factory('userAccessSignaturesStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    service.getForVisitor = function() {
      return $http.get(apiBaseUri + 'userAccessSignatures').catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.getForUser = function(userId) {
      return $http.get(apiBaseUri + 'userAccessSignatures/' + userId).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

//
// File upload API stub.
//
angular.module('webApp').factory('fileUploadStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    service.postUploadRequest = function(data) {
      return $http.post(apiBaseUri + 'files/uploadRequests', data).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    service.postUploadCompleteNotification = function(fileId) {
      return $http.post(apiBaseUri + 'files/uploadCompleteNotifications', fileId).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

//
// User state API stub.
//
angular.module('webApp').factory('userStateStub',
  function($http, $q, fifthweekConstants, utilities) {
    'use strict';

    var apiBaseUri = fifthweekConstants.apiBaseUri;
    var service = {};

    service.get = function(userId) {
      return $http.get(apiBaseUri + 'userState/' + userId).catch(function(response) {
        return $q.reject(utilities.getHttpError(response));
      });
    };

    return service;
  });

