describe('availability stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    target = $injector.get('availabilityStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should get', function() {

    var responseData = 'response data';
    $httpBackend.expectGET(fifthweekConstants.apiBaseUri + 'availability').respond(200, responseData);

    var result = null;
    target.get().then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should head', function() {

    var responseData = 'response data';
    $httpBackend.expectGET(fifthweekConstants.apiBaseUri + 'availability').respond(200, responseData);

    var result = null;
    target.head().then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

describe('channel stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    target = $injector.get('channelStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should post channel', function() {
    var newChannelData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'channels', newChannelData).respond(200, responseData);

    var result = null;
    target.postChannel(newChannelData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should put channel', function() {
    var channelId = 'value0';
    var channelData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(fifthweekConstants.apiBaseUri + 'channels/' + encodeURIComponent(channelId), channelData).respond(200, responseData);

    var result = null;
    target.putChannel(channelId, channelData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should delete channel', function() {
    var channelId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectDELETE(fifthweekConstants.apiBaseUri + 'channels/' + encodeURIComponent(channelId)).respond(200, responseData);

    var result = null;
    target.deleteChannel(channelId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

describe('collection stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    target = $injector.get('collectionStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should post collection', function() {
    var newCollectionData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'collections', newCollectionData).respond(200, responseData);

    var result = null;
    target.postCollection(newCollectionData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should put collection', function() {
    var collectionId = 'value0';
    var collectionData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(fifthweekConstants.apiBaseUri + 'collections/' + encodeURIComponent(collectionId), collectionData).respond(200, responseData);

    var result = null;
    target.putCollection(collectionId, collectionData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should delete collection', function() {
    var collectionId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectDELETE(fifthweekConstants.apiBaseUri + 'collections/' + encodeURIComponent(collectionId)).respond(200, responseData);

    var result = null;
    target.deleteCollection(collectionId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get live date of new queued post', function() {
    var collectionId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(fifthweekConstants.apiBaseUri + 'collections/' + encodeURIComponent(collectionId) + '/newQueuedPostLiveDate').respond(200, responseData);

    var result = null;
    target.getLiveDateOfNewQueuedPost(collectionId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

describe('end to end test inbox stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    target = $injector.get('endToEndTestInboxStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should get latest message and clear mailbox', function() {
    var mailboxName = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(fifthweekConstants.apiBaseUri + 'testMailboxes/' + encodeURIComponent(mailboxName)).respond(200, responseData);

    var result = null;
    target.getLatestMessageAndClearMailbox(mailboxName).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

describe('account settings stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    target = $injector.get('accountSettingsStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should get', function() {
    var userId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(fifthweekConstants.apiBaseUri + 'accountSettings/' + encodeURIComponent(userId)).respond(200, responseData);

    var result = null;
    target.get(userId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should put', function() {
    var userId = 'value0';
    var updatedAccountSettingsData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(fifthweekConstants.apiBaseUri + 'accountSettings/' + encodeURIComponent(userId), updatedAccountSettingsData).respond(200, responseData);

    var result = null;
    target.put(userId, updatedAccountSettingsData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

describe('membership stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    target = $injector.get('membershipStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should post registration', function() {
    var registrationData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'membership/registrations', registrationData).respond(200, responseData);

    var result = null;
    target.postRegistration(registrationData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get username availability', function() {
    var username = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(fifthweekConstants.apiBaseUri + 'membership/availableUsernames/' + encodeURIComponent(username)).respond(200, responseData);

    var result = null;
    target.getUsernameAvailability(username).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post password reset request', function() {
    var passwordResetRequestData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'membership/passwordResetRequests', passwordResetRequestData).respond(200, responseData);

    var result = null;
    target.postPasswordResetRequest(passwordResetRequestData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post password reset confirmation', function() {
    var passwordResetConfirmationData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'membership/passwordResetConfirmations', passwordResetConfirmationData).respond(200, responseData);

    var result = null;
    target.postPasswordResetConfirmation(passwordResetConfirmationData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get password reset token validity', function() {
    var userId = 'value0';
    var token = 'value1';

    var responseData = 'response data';
    $httpBackend.expectGET(fifthweekConstants.apiBaseUri + 'membership/passwordResetTokens/' + encodeURIComponent(userId) + '?' + (token === undefined ? '' : 'token=' + encodeURIComponent(token) + '&')).respond(200, responseData);

    var result = null;
    target.getPasswordResetTokenValidity(userId, token).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post registered interest', function() {
    var registerInterestData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'membership/registeredInterest', registerInterestData).respond(200, responseData);

    var result = null;
    target.postRegisteredInterest(registerInterestData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post identified user', function() {
    var identifiedUserData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'membership/identifiedUsers', identifiedUserData).respond(200, responseData);

    var result = null;
    target.postIdentifiedUser(identifiedUserData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

describe('log stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    target = $injector.get('logStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should post', function() {
    var logMessage = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'log', logMessage).respond(200, responseData);

    var result = null;
    target.post(logMessage).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

describe('posts stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    target = $injector.get('postsStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should get creator backlog', function() {
    var creatorId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(fifthweekConstants.apiBaseUri + 'posts/creatorBacklog/' + encodeURIComponent(creatorId)).respond(200, responseData);

    var result = null;
    target.getCreatorBacklog(creatorId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get creator newsfeed', function() {
    var creatorId = 'value0';
    var newsfeedPaginationData = {
      startIndex: 'value1-0',
      count: 'value1-1',
    };

    var responseData = 'response data';
    $httpBackend.expectGET(fifthweekConstants.apiBaseUri + 'posts/creatorNewsfeed/' + encodeURIComponent(creatorId) + '?' + (newsfeedPaginationData.startIndex === undefined ? '' : 'startIndex=' + encodeURIComponent(newsfeedPaginationData.startIndex) + '&') + (newsfeedPaginationData.count === undefined ? '' : 'count=' + encodeURIComponent(newsfeedPaginationData.count) + '&')).respond(200, responseData);

    var result = null;
    target.getCreatorNewsfeed(creatorId, newsfeedPaginationData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get newsfeed', function() {
    var filter = {
      creatorId: 'value0-0',
      origin: 'value0-1',
      searchForwards: 'value0-2',
      startIndex: 'value0-3',
      count: 'value0-4',
    };

    var responseData = 'response data';
    $httpBackend.expectGET(fifthweekConstants.apiBaseUri + 'posts/newsfeed?' + (filter.creatorId === undefined ? '' : 'creatorId=' + encodeURIComponent(filter.creatorId) + '&') + (filter.origin === undefined ? '' : 'origin=' + encodeURIComponent(filter.origin) + '&') + (filter.searchForwards === undefined ? '' : 'searchForwards=' + encodeURIComponent(filter.searchForwards) + '&') + (filter.startIndex === undefined ? '' : 'startIndex=' + encodeURIComponent(filter.startIndex) + '&') + (filter.count === undefined ? '' : 'count=' + encodeURIComponent(filter.count) + '&')).respond(200, responseData);

    var result = null;
    target.getNewsfeed(filter).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should delete post', function() {
    var postId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectDELETE(fifthweekConstants.apiBaseUri + 'posts/' + encodeURIComponent(postId)).respond(200, responseData);

    var result = null;
    target.deletePost(postId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post new queue order', function() {
    var collectionId = 'value0';
    var newQueueOrder = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'posts/queues/' + encodeURIComponent(collectionId), newQueueOrder).respond(200, responseData);

    var result = null;
    target.postNewQueueOrder(collectionId, newQueueOrder).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post to queue', function() {
    var postId = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'posts/queued', JSON.stringify(postId)).respond(200, responseData);

    var result = null;
    target.postToQueue(postId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post to live', function() {
    var postId = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'posts/live', JSON.stringify(postId)).respond(200, responseData);

    var result = null;
    target.postToLive(postId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should put live date', function() {
    var postId = 'value0';
    var newLiveDate = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(fifthweekConstants.apiBaseUri + 'posts/' + encodeURIComponent(postId) + '/liveDate', newLiveDate).respond(200, responseData);

    var result = null;
    target.putLiveDate(postId, newLiveDate).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post note', function() {
    var noteData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'posts/notes', noteData).respond(200, responseData);

    var result = null;
    target.postNote(noteData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should put note', function() {
    var postId = 'value0';
    var noteData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(fifthweekConstants.apiBaseUri + 'posts/notes/' + encodeURIComponent(postId), noteData).respond(200, responseData);

    var result = null;
    target.putNote(postId, noteData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post image', function() {
    var imageData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'posts/images', imageData).respond(200, responseData);

    var result = null;
    target.postImage(imageData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should put image', function() {
    var postId = 'value0';
    var imageData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(fifthweekConstants.apiBaseUri + 'posts/images/' + encodeURIComponent(postId), imageData).respond(200, responseData);

    var result = null;
    target.putImage(postId, imageData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post file', function() {
    var fileData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'posts/files', fileData).respond(200, responseData);

    var result = null;
    target.postFile(fileData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should put file', function() {
    var postId = 'value0';
    var fileData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(fifthweekConstants.apiBaseUri + 'posts/files/' + encodeURIComponent(postId), fileData).respond(200, responseData);

    var result = null;
    target.putFile(postId, fileData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

describe('blog access stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    target = $injector.get('blogAccessStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should put free access list', function() {
    var blogId = 'value0';
    var data = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(fifthweekConstants.apiBaseUri + 'blogAccess/freeAccessList/' + encodeURIComponent(blogId), data).respond(200, responseData);

    var result = null;
    target.putFreeAccessList(blogId, data).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get free access list', function() {
    var blogId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(fifthweekConstants.apiBaseUri + 'blogAccess/freeAccessList/' + encodeURIComponent(blogId)).respond(200, responseData);

    var result = null;
    target.getFreeAccessList(blogId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

describe('subscription stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    target = $injector.get('subscriptionStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should put blog subscriptions', function() {
    var blogId = 'value0';
    var subscriptionData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(fifthweekConstants.apiBaseUri + 'subscriptions/blogs/' + encodeURIComponent(blogId), subscriptionData).respond(200, responseData);

    var result = null;
    target.putBlogSubscriptions(blogId, subscriptionData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should delete channel subscription', function() {
    var channelId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectDELETE(fifthweekConstants.apiBaseUri + 'subscriptions/channels/' + encodeURIComponent(channelId)).respond(200, responseData);

    var result = null;
    target.deleteChannelSubscription(channelId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should put channel subscription', function() {
    var channelId = 'value0';
    var subscriptionData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(fifthweekConstants.apiBaseUri + 'subscriptions/channels/' + encodeURIComponent(channelId), subscriptionData).respond(200, responseData);

    var result = null;
    target.putChannelSubscription(channelId, subscriptionData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

describe('blog stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    target = $injector.get('blogStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should post blog', function() {
    var blogData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'blogs', blogData).respond(200, responseData);

    var result = null;
    target.postBlog(blogData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should put blog', function() {
    var blogId = 'value0';
    var blogData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(fifthweekConstants.apiBaseUri + 'blogs/' + encodeURIComponent(blogId), blogData).respond(200, responseData);

    var result = null;
    target.putBlog(blogId, blogData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get landing page', function() {
    var username = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(fifthweekConstants.apiBaseUri + 'blogs/landingPages/' + encodeURIComponent(username)).respond(200, responseData);

    var result = null;
    target.getLandingPage(username).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

describe('file upload stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    target = $injector.get('fileUploadStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should post upload request', function() {
    var data = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'files/uploadRequests', data).respond(200, responseData);

    var result = null;
    target.postUploadRequest(data).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post upload complete notification', function() {
    var fileId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectPOST(fifthweekConstants.apiBaseUri + 'files/uploadCompleteNotifications/' + encodeURIComponent(fileId)).respond(200, responseData);

    var result = null;
    target.postUploadCompleteNotification(fileId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

describe('user state stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    target = $injector.get('userStateStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should get user state', function() {
    var userId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(fifthweekConstants.apiBaseUri + 'userState/' + encodeURIComponent(userId)).respond(200, responseData);

    var result = null;
    target.getUserState(userId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get visitor state', function() {

    var responseData = 'response data';
    $httpBackend.expectGET(fifthweekConstants.apiBaseUri + 'userState').respond(200, responseData);

    var result = null;
    target.getVisitorState().then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

