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

describe('queue stub', function() {
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
    target = $injector.get('queueStub');
  }));

  afterEach(function() {
    $httpBackend.verifyNoOutstandingExpectation();
    $httpBackend.verifyNoOutstandingRequest();
  });

  it('should post queue', function() {
    var newQueueData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'queues', newQueueData)).respond(200, responseData);

    var result = null;
    target.postQueue(newQueueData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should put queue', function() {
    var queueId = 'value0';
    var queueData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'queues/' + encodeURIComponent(queueId), queueData)).respond(200, responseData);

    var result = null;
    target.putQueue(queueId, queueData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should delete queue', function() {
    var queueId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectDELETE(utilities.fixUri(fifthweekConstants.apiBaseUri + 'queues/' + encodeURIComponent(queueId))).respond(200, responseData);

    var result = null;
    target.deleteQueue(queueId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get live date of new queued post', function() {
    var queueId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'queues/' + encodeURIComponent(queueId) + '/newQueuedPostLiveDate')).respond(200, responseData);

    var result = null;
    target.getLiveDateOfNewQueuedPost(queueId).then(function(response) { result = response.data; });

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

  it('should post feedback', function() {
    var feedbackData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'membership/feedback', feedbackData)).respond(200, responseData);

    var result = null;
    target.postFeedback(feedbackData).then(function(response) { result = response.data; });

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

describe('post stub', function() {
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
    target = $injector.get('postStub');
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

  it('should get newsfeed', function() {
    var filterData = {
      creatorId: 'value0-0',
      channelId: 'value0-1',
      origin: 'value0-2',
      searchForwards: 'value0-3',
      startIndex: 'value0-4',
      count: 'value0-5',
    };

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/newsfeed?' + (filterData.creatorId === undefined ? '' : 'creatorId=' + encodeURIComponent(filterData.creatorId) + '&') + (filterData.channelId === undefined ? '' : 'channelId=' + encodeURIComponent(filterData.channelId) + '&') + (filterData.origin === undefined ? '' : 'origin=' + encodeURIComponent(filterData.origin) + '&') + (filterData.searchForwards === undefined ? '' : 'searchForwards=' + encodeURIComponent(filterData.searchForwards) + '&') + (filterData.startIndex === undefined ? '' : 'startIndex=' + encodeURIComponent(filterData.startIndex) + '&') + (filterData.count === undefined ? '' : 'count=' + encodeURIComponent(filterData.count) + '&'))).respond(200, responseData);

    var result = null;
    target.getNewsfeed(filterData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get preview newsfeed', function() {
    var filterData = {
      creatorId: 'value0-0',
      channelId: 'value0-1',
      origin: 'value0-2',
      searchForwards: 'value0-3',
      startIndex: 'value0-4',
      count: 'value0-5',
    };

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/previewNewsfeed?' + (filterData.creatorId === undefined ? '' : 'creatorId=' + encodeURIComponent(filterData.creatorId) + '&') + (filterData.channelId === undefined ? '' : 'channelId=' + encodeURIComponent(filterData.channelId) + '&') + (filterData.origin === undefined ? '' : 'origin=' + encodeURIComponent(filterData.origin) + '&') + (filterData.searchForwards === undefined ? '' : 'searchForwards=' + encodeURIComponent(filterData.searchForwards) + '&') + (filterData.startIndex === undefined ? '' : 'startIndex=' + encodeURIComponent(filterData.startIndex) + '&') + (filterData.count === undefined ? '' : 'count=' + encodeURIComponent(filterData.count) + '&'))).respond(200, responseData);

    var result = null;
    target.getPreviewNewsfeed(filterData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post post', function() {
    var postData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts', postData)).respond(200, responseData);

    var result = null;
    target.postPost(postData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should put post', function() {
    var postId = 'value0';
    var postData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/' + encodeURIComponent(postId), postData)).respond(200, responseData);

    var result = null;
    target.putPost(postId, postData).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get post', function() {
    var postId = 'value0';
    var requestFreePost = 'value1';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/' + encodeURIComponent(postId) + '?' + (requestFreePost === undefined ? '' : 'requestFreePost=' + encodeURIComponent(requestFreePost) + '&'))).respond(200, responseData);

    var result = null;
    target.getPost(postId, requestFreePost).then(function(response) { result = response.data; });

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
    var queueId = 'value0';
    var newQueueOrder = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/queues/' + encodeURIComponent(queueId), newQueueOrder)).respond(200, responseData);

    var result = null;
    target.postNewQueueOrder(queueId, newQueueOrder).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should put queue', function() {
    var postId = 'value0';
    var queueId = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPUT(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/' + encodeURIComponent(postId) + '/queue', JSON.stringify(queueId))).respond(200, responseData);

    var result = null;
    target.putQueue(postId, queueId).then(function(response) { result = response.data; });

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

  it('should post comment', function() {
    var postId = 'value0';
    var comment = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/' + encodeURIComponent(postId) + '/comments', comment)).respond(200, responseData);

    var result = null;
    target.postComment(postId, comment).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should get comments', function() {
    var postId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/' + encodeURIComponent(postId) + '/comments')).respond(200, responseData);

    var result = null;
    target.getComments(postId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should post like', function() {
    var postId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/' + encodeURIComponent(postId) + '/likes')).respond(200, responseData);

    var result = null;
    target.postLike(postId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });

  it('should delete like', function() {
    var postId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectDELETE(utilities.fixUri(fifthweekConstants.apiBaseUri + 'posts/' + encodeURIComponent(postId) + '/likes')).respond(200, responseData);

    var result = null;
    target.deleteLike(postId).then(function(response) { result = response.data; });

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

  it('should post channel subscription', function() {
    var channelId = 'value0';
    var subscriptionData = 'value-body';

    var responseData = 'response data';
    $httpBackend.expectPOST(utilities.fixUri(fifthweekConstants.apiBaseUri + 'subscriptions/channels/' + encodeURIComponent(channelId), subscriptionData)).respond(200, responseData);

    var result = null;
    target.postChannelSubscription(channelId, subscriptionData).then(function(response) { result = response.data; });

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

  it('should get creator revenues', function() {

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'blogs/creatorRevenues')).respond(200, responseData);

    var result = null;
    target.getCreatorRevenues().then(function(response) { result = response.data; });

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
    var impersonate = 'value1';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'userState/' + encodeURIComponent(userId) + '?' + (impersonate === undefined ? '' : 'impersonate=' + encodeURIComponent(impersonate) + '&'))).respond(200, responseData);

    var result = null;
    target.getUserState(userId, impersonate).then(function(response) { result = response.data; });

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

  it('should get payment processing lease', function() {
    var leaseId = 'value0';

    var responseData = 'response data';
    $httpBackend.expectGET(utilities.fixUri(fifthweekConstants.apiBaseUri + 'payment/lease?' + (leaseId === undefined ? '' : 'leaseId=' + encodeURIComponent(leaseId) + '&'))).respond(200, responseData);

    var result = null;
    target.getPaymentProcessingLease(leaseId).then(function(response) { result = response.data; });

    $httpBackend.flush();
    $rootScope.$apply();

    expect(result).toBe(responseData);
  });
});

