describe('availability stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;
  var utilities;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    utilities = $injector.get('utilities');
    target = $injector.get('availabilityStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should get', function() {

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'availability')).respond(200, responseData);

    var result = null;
    target.get().then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should head', function() {

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'availability')).respond(200, responseData);

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
  var utilities;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    utilities = $injector.get('utilities');
    target = $injector.get('channelStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should post channel', function() {
    var newChannelData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'channels', newChannelData)).respond(200, responseData);

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
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'channels/' + encodeURIComponent(channelId), channelData)).respond(200, responseData);

    var result = null;
    target.putChannel(channelId, channelData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should delete channel', function() {
    var channelId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectDELETE(utilities.fixUri(fifthweekConstants.apiBaseUri + 'channels/' + encodeURIComponent(channelId))).respond(200, responseData);

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
  var utilities;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    utilities = $injector.get('utilities');
    target = $injector.get('collectionStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should post collection', function() {
    var newCollectionData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'collections', newCollectionData)).respond(200, responseData);

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
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'collections/' + encodeURIComponent(collectionId), collectionData)).respond(200, responseData);

    var result = null;
    target.putCollection(collectionId, collectionData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should delete collection', function() {
    var collectionId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectDELETE(utilities.fixUri(fifthweekConstants.apiBaseUri + 'collections/' + encodeURIComponent(collectionId))).respond(200, responseData);

    var result = null;
    target.deleteCollection(collectionId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get live date of new queued post', function() {
    var collectionId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'collections/' + encodeURIComponent(collectionId) + '/newQueuedPostLiveDate')).respond(200, responseData);

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
  var utilities;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    utilities = $injector.get('utilities');
    target = $injector.get('endToEndTestInboxStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should get latest message and clear mailbox', function() {
    var mailboxName = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'testMailboxes/' + encodeURIComponent(mailboxName))).respond(200, responseData);

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
  var utilities;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    utilities = $injector.get('utilities');
    target = $injector.get('accountSettingsStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should get', function() {
    var userId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'accountSettings/' + encodeURIComponent(userId))).respond(200, responseData);

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
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'accountSettings/' + encodeURIComponent(userId), updatedAccountSettingsData)).respond(200, responseData);

    var result = null;
    target.put(userId, updatedAccountSettingsData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should put creator information', function() {
    var userId = 'value0';
    var creatorInformation = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'accountSettings/' + encodeURIComponent(userId) + '/creatorInformation', creatorInformation)).respond(200, responseData);

    var result = null;
    target.putCreatorInformation(userId, creatorInformation).then(function(response) { result = response.data; });

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
  var utilities;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    utilities = $injector.get('utilities');
    target = $injector.get('membershipStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should post registration', function() {
    var registrationData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'membership/registrations', registrationData)).respond(200, responseData);

    var result = null;
    target.postRegistration(registrationData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get username availability', function() {
    var username = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'membership/availableUsernames/' + encodeURIComponent(username))).respond(200, responseData);

    var result = null;
    target.getUsernameAvailability(username).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post password reset request', function() {
    var passwordResetRequestData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'membership/passwordResetRequests', passwordResetRequestData)).respond(200, responseData);

    var result = null;
    target.postPasswordResetRequest(passwordResetRequestData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post password reset confirmation', function() {
    var passwordResetConfirmationData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'membership/passwordResetConfirmations', passwordResetConfirmationData)).respond(200, responseData);

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
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'membership/passwordResetTokens/' + encodeURIComponent(userId) + '?' + (token === undefined ? '' : 'token=' + encodeURIComponent(token) + '&'))).respond(200, responseData);

    var result = null;
    target.getPasswordResetTokenValidity(userId, token).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post registered interest', function() {
    var registerInterestData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'membership/registeredInterest', registerInterestData)).respond(200, responseData);

    var result = null;
    target.postRegisteredInterest(registerInterestData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post identified user', function() {
    var identifiedUserData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'membership/identifiedUsers', identifiedUserData)).respond(200, responseData);

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
  var utilities;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    utilities = $injector.get('utilities');
    target = $injector.get('logStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should post', function() {
    var logMessage = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'log', logMessage)).respond(200, responseData);

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
  var utilities;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    utilities = $injector.get('utilities');
    target = $injector.get('postsStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should get creator backlog', function() {
    var creatorId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/creatorBacklog/' + encodeURIComponent(creatorId))).respond(200, responseData);

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
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/creatorNewsfeed/' + encodeURIComponent(creatorId) + '?' + (newsfeedPaginationData.startIndex === undefined ? '' : 'startIndex=' + encodeURIComponent(newsfeedPaginationData.startIndex) + '&') + (newsfeedPaginationData.count === undefined ? '' : 'count=' + encodeURIComponent(newsfeedPaginationData.count) + '&'))).respond(200, responseData);

    var result = null;
    target.getCreatorNewsfeed(creatorId, newsfeedPaginationData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get newsfeed', function() {
    var filter = {
      creatorId: 'value0-0',
      channelId: 'value0-1',
      collectionId: 'value0-2',
      origin: 'value0-3',
      searchForwards: 'value0-4',
      startIndex: 'value0-5',
      count: 'value0-6',
    };

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/newsfeed?' + (filter.creatorId === undefined ? '' : 'creatorId=' + encodeURIComponent(filter.creatorId) + '&') + (filter.channelId === undefined ? '' : 'channelId=' + encodeURIComponent(filter.channelId) + '&') + (filter.collectionId === undefined ? '' : 'collectionId=' + encodeURIComponent(filter.collectionId) + '&') + (filter.origin === undefined ? '' : 'origin=' + encodeURIComponent(filter.origin) + '&') + (filter.searchForwards === undefined ? '' : 'searchForwards=' + encodeURIComponent(filter.searchForwards) + '&') + (filter.startIndex === undefined ? '' : 'startIndex=' + encodeURIComponent(filter.startIndex) + '&') + (filter.count === undefined ? '' : 'count=' + encodeURIComponent(filter.count) + '&'))).respond(200, responseData);

    var result = null;
    target.getNewsfeed(filter).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should delete post', function() {
    var postId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectDELETE(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/' + encodeURIComponent(postId))).respond(200, responseData);

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
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/queues/' + encodeURIComponent(collectionId), newQueueOrder)).respond(200, responseData);

    var result = null;
    target.postNewQueueOrder(collectionId, newQueueOrder).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post to queue', function() {
    var postId = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/queued', JSON.stringify(postId))).respond(200, responseData);

    var result = null;
    target.postToQueue(postId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post to live', function() {
    var postId = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/live', JSON.stringify(postId))).respond(200, responseData);

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
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/' + encodeURIComponent(postId) + '/liveDate', newLiveDate)).respond(200, responseData);

    var result = null;
    target.putLiveDate(postId, newLiveDate).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post note', function() {
    var noteData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/notes', noteData)).respond(200, responseData);

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
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/notes/' + encodeURIComponent(postId), noteData)).respond(200, responseData);

    var result = null;
    target.putNote(postId, noteData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post image', function() {
    var imageData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/images', imageData)).respond(200, responseData);

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
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/images/' + encodeURIComponent(postId), imageData)).respond(200, responseData);

    var result = null;
    target.putImage(postId, imageData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post file', function() {
    var fileData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/files', fileData)).respond(200, responseData);

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
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/files/' + encodeURIComponent(postId), fileData)).respond(200, responseData);

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
  var utilities;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    utilities = $injector.get('utilities');
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
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'blogAccess/freeAccessList/' + encodeURIComponent(blogId), data)).respond(200, responseData);

    var result = null;
    target.putFreeAccessList(blogId, data).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get free access list', function() {
    var blogId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'blogAccess/freeAccessList/' + encodeURIComponent(blogId))).respond(200, responseData);

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
  var utilities;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    utilities = $injector.get('utilities');
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
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'subscriptions/blogs/' + encodeURIComponent(blogId), subscriptionData)).respond(200, responseData);

    var result = null;
    target.putBlogSubscriptions(blogId, subscriptionData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should delete channel subscription', function() {
    var channelId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectDELETE(utilities.fixUri(fifthweekConstants.apiBaseUri + 'subscriptions/channels/' + encodeURIComponent(channelId))).respond(200, responseData);

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
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'subscriptions/channels/' + encodeURIComponent(channelId), subscriptionData)).respond(200, responseData);

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
  var utilities;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    utilities = $injector.get('utilities');
    target = $injector.get('blogStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should post blog', function() {
    var blogData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'blogs', blogData)).respond(200, responseData);

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
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'blogs/' + encodeURIComponent(blogId), blogData)).respond(200, responseData);

    var result = null;
    target.putBlog(blogId, blogData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get landing page', function() {
    var username = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'blogs/landingPages/' + encodeURIComponent(username))).respond(200, responseData);

    var result = null;
    target.getLandingPage(username).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get subscriber information', function() {
    var blogId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'blogs/subscribers/' + encodeURIComponent(blogId))).respond(200, responseData);

    var result = null;
    target.getSubscriberInformation(blogId).then(function(response) { result = response.data; });

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
  var utilities;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    utilities = $injector.get('utilities');
    target = $injector.get('fileUploadStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should post upload request', function() {
    var data = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'files/uploadRequests', data)).respond(200, responseData);

    var result = null;
    target.postUploadRequest(data).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post upload complete notification', function() {
    var data = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'files/uploadCompleteNotifications', data)).respond(200, responseData);

    var result = null;
    target.postUploadCompleteNotification(data).then(function(response) { result = response.data; });

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
  var utilities;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    utilities = $injector.get('utilities');
    target = $injector.get('userStateStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should get user state', function() {
    var userId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'userState/' + encodeURIComponent(userId))).respond(200, responseData);

    var result = null;
    target.getUserState(userId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get visitor state', function() {

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'userState')).respond(200, responseData);

    var result = null;
    target.getVisitorState().then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

describe('payments stub', function() {
  'use strict';

  var fifthweekConstants;
  var $httpBackend;
  var $rootScope;
  var target;
  var utilities;

  beforeEach(module('webApp', 'stateMock'));

  beforeEach(inject(function($injector) {
    fifthweekConstants = $injector.get('fifthweekConstants');
    $httpBackend = $injector.get('$httpBackend');
    $rootScope = $injector.get('$rootScope');
    utilities = $injector.get('utilities');
    target = $injector.get('paymentsStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should put payment origin', function() {
    var userId = 'value0';
    var data = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'payment/origins/' + encodeURIComponent(userId), data)).respond(200, responseData);

    var result = null;
    target.putPaymentOrigin(userId, data).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post credit request', function() {
    var userId = 'value0';
    var data = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'payment/creditRequests/' + encodeURIComponent(userId), data)).respond(200, responseData);

    var result = null;
    target.postCreditRequest(userId, data).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get credit request summary', function() {
    var userId = 'value0';
    var countryCode = 'value1';
    var creditCardPrefix = 'value2';
    var ipAddress = 'value3';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'payment/creditRequestSummaries/' + encodeURIComponent(userId) + '?' + (countryCode === undefined ? '' : 'countryCode=' + encodeURIComponent(countryCode) + '&') + (creditCardPrefix === undefined ? '' : 'creditCardPrefix=' + encodeURIComponent(creditCardPrefix) + '&') + (ipAddress === undefined ? '' : 'ipAddress=' + encodeURIComponent(ipAddress) + '&'))).respond(200, responseData);

    var result = null;
    target.getCreditRequestSummary(userId, countryCode, creditCardPrefix, ipAddress).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should delete payment information', function() {
    var userId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectDELETE(utilities.fixUri(fifthweekConstants.apiBaseUri + 'payment/paymentInformation/' + encodeURIComponent(userId))).respond(200, responseData);

    var result = null;
    target.deletePaymentInformation(userId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post transaction refund', function() {
    var transactionReference = 'value0';
    var data = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'payment/transactionRefunds/' + encodeURIComponent(transactionReference), data)).respond(200, responseData);

    var result = null;
    target.postTransactionRefund(transactionReference, data).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post credit refund', function() {
    var transactionReference = 'value0';
    var data = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'payment/creditRefunds/' + encodeURIComponent(transactionReference), data)).respond(200, responseData);

    var result = null;
    target.postCreditRefund(transactionReference, data).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get transactions', function() {
    var userId = 'value0';
    var startTimeInclusive = 'value1';
    var endTimeExclusive = 'value2';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'payment/transactions?' + (userId === undefined ? '' : 'userId=' + encodeURIComponent(userId) + '&') + (startTimeInclusive === undefined ? '' : 'startTimeInclusive=' + encodeURIComponent(startTimeInclusive) + '&') + (endTimeExclusive === undefined ? '' : 'endTimeExclusive=' + encodeURIComponent(endTimeExclusive) + '&'))).respond(200, responseData);

    var result = null;
    target.getTransactions(userId, startTimeInclusive, endTimeExclusive).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

