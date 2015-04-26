using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypetalkSharp;
using System.Linq;

namespace TTSharpTest
{
    [TestClass]
    public partial class UnitTest
    {
        [TestMethod]
        public void AuthorizationTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
        }
        [TestMethod]
        public void AuthorizationUpdateTest() {
            var tts = new TypetalkClient(
                Credential.ClientId,
                Credential.Secrets,
                Credential.OldAccessToken,
                Credential.RefleshToken);

            var t = tts.Topics.All().Result;
        }

        [TestMethod]
        public void GetTopicsTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicsRoot = tts.Topics.All().Result;
            Assert.IsTrue(topicsRoot.Count() > 0);

            foreach(var topics in topicsRoot) {
                Assert.IsNotNull(topics.Favorite);
                Assert.IsNotNull(topics.Unread);

                Assert.IsNotNull(topics.Topic.Id);
                Assert.IsNotNull(topics.Topic.Name);
                Assert.IsNotNull(topics.Topic.Suggestion);

                Assert.IsNotNull(topics.Unread.Count);
                Assert.IsNotNull(topics.Unread.PostId);
                Assert.IsNotNull(topics.Unread.TopicId);
            }
        }

        [TestMethod]
        public void GetTopicMessagesTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicsRoot = tts.Topics.All().Result;
            var messagsRoot = tts.Topics.Messages.Get(
                                    topicsRoot.First(x => x.Topic.Name == "TTSharpTest").Topic.Id
                              ).Result;
            Assert.IsNotNull(messagsRoot.Posts);
            Assert.IsNotNull(messagsRoot.Topic);
            //Assert.IsNotNull(messagsRoot.Team);
        }

        [TestMethod]
        public void GetTopicMessagesWithParamTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicId = tts.Topics.All().Result.First(x => x.Topic.Name == "TTSharpTest").Topic.Id;
            var param = new MessageRequest()
            {
                Count = 3,
                Direction = MessageRequest.MessageRequestDirection.Forward,
            };
            var messagesRoot = tts.Topics.Messages.Get(topicId, param).Result;
            Assert.IsTrue(messagesRoot.Posts.Count() == 3);
        }
        [TestMethod]
        public void PostMessageTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicsRoot = tts.Topics.All().Result;
            var mess = new NewMessge("test Message");
            var topicId = topicsRoot.First(x => x.Topic.Name == "TTSharpTest").Topic.Id;
            // only post
            var res = tts.Topics.Messages.Post(topicId, mess).Result;
            Assert.IsNotNull(res.Post);
            Assert.IsNotNull(res.Topic);

            // post and add talk
            var talk = tts.Topics.Talks.All(topicId).Result.ToArray()[0].Id;
            mess.TalkId[0] = talk;
            res = tts.Topics.Messages.Post(topicId, mess).Result;
            Assert.IsNotNull(res.Post);
            Assert.IsNotNull(res.Topic);
            Assert.IsNotNull(res.Post.Talks.First(x => x.Id == mess.TalkId[0]));

            // post as reply
            var rep = new NewMessge("rep")
            {
                ReplyTo = res.Post.Id
            };
            res = tts.Topics.Messages.Post(topicId, rep).Result;
            Assert.IsNotNull(res.Post);
            Assert.IsNotNull(res.Topic);
            Assert.IsNotNull(res.Post.ReplyTo);
        }

        [TestMethod]
        public void GetMembersTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicsRoot = tts.Topics.All().Result;
            var messagesRoot = tts.Topics.Members(topicsRoot.First(x => x.Topic.Name == "TTSharpTest").Topic.Id).Result;
            Assert.IsNotNull(messagesRoot);
            Assert.IsNotNull(messagesRoot.Accounts);
        }

        [TestMethod]
        public void GetMessageDetailTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicsRoot = tts.Topics.All().Result;
            var topicId = topicsRoot.First(x => x.Topic.Name == "TTSharpTest").Topic.Id;
            var messages = tts.Topics.Messages.Get(topicId).Result;
            var messagesRoot = tts.Topics.Messages.Details(topicId, messages.Posts.Last().Id).Result;
            Assert.IsNotNull(messagesRoot);
        }

        [TestMethod]
        public void DeleteMessageTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicsRoot = tts.Topics.All().Result;
            var topicId = topicsRoot.First(x => x.Topic.Name == "TTSharpTest").Topic.Id;
            var messages = tts.Topics.Messages.Get(topicId).Result;
            var messagesRoot = tts.Topics.Messages.Delete(topicId, messages.Posts.Last().Id).Result;

            Assert.IsNotNull(messagesRoot);
            Assert.IsNotNull(messagesRoot.CreatedAt);
            Assert.IsNotNull(messagesRoot.UpdatedAt);
            Assert.IsFalse(messagesRoot.Id == 0);
            Assert.IsFalse(messagesRoot.TopicId == 0);
            Assert.IsNotNull(messagesRoot.Account);
        }

        [TestMethod]
        public void LikeMessageTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicsRoot = tts.Topics.All().Result;
            var topicId = topicsRoot.First(x => x.Topic.Name == "TTSharpTest").Topic.Id;
            var messages = tts.Topics.Messages.Get(topicId).Result;
            var messagesRoot = tts.Topics.Messages.Like(topicId, messages.Posts.First(x => x.Likes.Count() == 0).Id).Result;

            Assert.IsNotNull(messagesRoot);
            Assert.IsNotNull(messagesRoot.Account);
            Assert.IsFalse(messagesRoot.Id == 0);
            Assert.IsFalse(messagesRoot.PostId == 0);
            Assert.IsFalse(messagesRoot.TopicId == 0);
        }

        [TestMethod]
        public void UnlikeMessageTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicsRoot = tts.Topics.All().Result;
            var topicId = topicsRoot.First(x => x.Topic.Name == "TTSharpTest").Topic.Id;
            var messages = tts.Topics.Messages.Get(topicId).Result;
            var messagesRoot = tts.Topics.Messages.UnLike(topicId, messages.Posts.First(x => x.Likes.Count() != 0).Id).Result;

            Assert.IsNotNull(messagesRoot);
            Assert.IsNotNull(messagesRoot.Account);
            Assert.IsFalse(messagesRoot.Id == 0);
            Assert.IsFalse(messagesRoot.PostId == 0);
            Assert.IsFalse(messagesRoot.TopicId == 0);
        }

        [TestMethod]
        public void GetAllNotificationTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var notifications = tts.Notifications.All().Result;

            //Assert.IsNotNull(notifications.Invites);
            //Assert.IsNotNull(notifications.Mentions);
        }

        [TestMethod]
        public void GetUnreadNotificationTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var unreads = tts.Notifications.Unreads().Result;


        }

        [TestMethod]
        public void ReadNotificationTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var response = tts.Notifications.Read().Result;

            Assert.IsNotNull(response.Access);
        }

        [TestMethod]
        public void GetTopicDetailsTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicsRoot = tts.Topics.All().Result;
            var topicId = topicsRoot.First(x => x.Topic.Name == "TTSharpTest").Topic.Id;
            var detail = tts.Topics.Detail(topicId).Result;

            Assert.IsNotNull(detail.Accounts);
            Assert.IsNotNull(detail.Teams);
            Assert.IsNotNull(detail.Topic);
        }

        [TestMethod]
        public void GetAllTalksTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicsRoot = tts.Topics.All().Result;
            var topicId = topicsRoot.First(x => x.Topic.Name == "TTSharpTest").Topic.Id;
            var talks = tts.Topics.Talks.All(topicId).Result;

            Assert.IsNotNull(talks);
            Assert.IsNotNull(talks.First().CreatedAt);
            Assert.IsNotNull(talks.First().UpdatedAt);
            Assert.IsFalse(talks.First().Id == 0);
            Assert.IsFalse(talks.First().TopicId == 0);
            Assert.IsFalse(talks.First().Name == "");
        }

        [TestMethod]
        public void GetTalkMessages() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicsRoot = tts.Topics.All().Result;
            var topicId = topicsRoot.First(x => x.Topic.Name == "TTSharpTest").Topic.Id;
            var talkId = tts.Topics.Talks.All(topicId).Result.First().Id;

            var talks = tts.Topics.Talks.TalkMessages(topicId, talkId).Result;

            Assert.IsNotNull(talks.Talk);
            Assert.IsNotNull(talks.Posts);
            Assert.IsNotNull(talks.Topic);
        }
        [TestMethod]
        public void UploadFileTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicid = tts.Topics.All().Result.First(x => x.Topic.Name == "TTSharpTest").Topic.Id;

            var bytes = System.IO.File.ReadAllBytes(@"..\..\typetalk.png");
            var uploadResult = tts.Topics.Attachments.Upload(topicid, "test.jpg", bytes).Result;
            Assert.IsNotNull(uploadResult);
            var param = new NewMessge("test Message") {
               FileKey = new string[] {uploadResult.FileKey}
            };
            var res = tts.Topics.Messages.Post(topicid, param).Result;
            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Post);
            Assert.IsNotNull(res.Topic);
        }

        [TestMethod]
        public void CreateTopicWithMembersTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var name = "";
            var details = tts.Topics.Create(new NewTopic("test-with-members")
            {
                InviteMembers = new string[] {name},
                InviteMessages = "welcome to typetalksharp"
            }).Result;

            Assert.IsNotNull(details);
            Assert.IsNotNull(details.Accounts.First(x => x.Name == name));
        }
        [TestMethod]
        public void CreateTopicWithTeamTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var teamId = 0;
            var details = tts.Topics.Create(new NewTopic("test-with-entry")
            {
                InviteMessages = "welcome to typetalkSharp",
                TeamId = teamId
            }).Result;
            Assert.IsNotNull(details);
            Assert.IsNotNull(details.Teams.First(x => x.Team.Id == teamId));
        }
        [TestMethod]
        public void DeleteTopicTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicId = tts.Topics.Create("ttdelete-test").Result.Topic.Id;
            var detail = tts.Topics.Delete(topicId).Result;
            Assert.IsNotNull(detail);
            Assert.IsTrue(detail.Id == topicId);
        }

        [TestMethod]
        public void UpdateTopicTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicId = tts.Topics.All().Result.First(x => x.Topic.Name == "TTSharpTest").Topic.Id;
            var teamId = 1819;
            var res = tts.Topics.Update(topicId, new UpdateTopic("Typetalk-Sharp")
            {
                TeamId = teamId
            }).Result;

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Teams.First(x => x.Team.Id == teamId));
        }

        [TestMethod]
        public void AcceptTeamTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var notif = tts.Notifications.All().Result;
            foreach(var entry in notif.Invites.Teams) {
                var inviteId = entry.Id;
                var teamId = entry.Team.Id;
                var res = tts.Teams.Accept(entry.Team.Id, inviteId).Result;
                Assert.IsNotNull(res);
            }
        }

        [TestMethod]
        public void DeclineTeamTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var notif = tts.Notifications.All().Result;
            foreach(var entry in notif.Invites.Teams) {
                var inviteId = entry.Id;
                var teamId = entry.Team.Id;
                var res = tts.Teams.Decline(entry.Team.Id, inviteId).Result;
                Assert.IsNotNull(res);
            }
        }

        [TestMethod]
        public void AcceptTopicTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var notif = tts.Notifications.All().Result;
            foreach(var entry in notif.Invites.Topics) {
                var inviteId = entry.Id;
                var topicId = entry.Topic.Id;
                var res = tts.Topics.Accept(topicId, inviteId);
                Assert.IsNotNull(res);
            }
        }

        [TestMethod]
        public void DeclineTopicTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var notif = tts.Notifications.All().Result;
            foreach(var entry in notif.Invites.Topics) {
                var inviteId = entry.Id;
                var topicId = entry.Topic.Id;
                var res = tts.Topics.Decline(topicId, inviteId);
                Assert.IsNotNull(res);
            }
        }

        [TestMethod]
        public void GetMyProfileTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var prof = tts.Profile.Mine().Result;
            Assert.IsNotNull(prof);
            Assert.IsFalse(prof.Account.Id == 0);
            Assert.IsFalse(prof.Account.Name == string.Empty);
            Assert.IsFalse(prof.Account.FullName == string.Empty);
        }

        [TestMethod]
        public void CancelInvitationTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicId = tts.Topics.All().Result.First(x => x.Topic.Name == "TTSharpTest").Topic.Id;
            var details = tts.Topics.Detail(topicId).Result;
            var res = tts.Topics.CancelInvitation(topicId, new int[] {details.Invites.ToArray()[0].Id}).Result;
            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Accounts);
        }

        [TestMethod]
        public void RemoveMemberTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var topicId = tts.Topics.All().Result.First(x => x.Topic.Name == "TTSharpTest").Topic.Id;
            var details = tts.Topics.Detail(topicId).Result;
            var res = tts.Topics.RemoveMembers(topicId, new int[] { details.Accounts.ToArray()[0].Id }).Result;
            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Accounts);
        }

        [TestMethod]
        public void SearchUserTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var res = tts.Search.Accounts("mujirushick").Result;
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetFriendsListTest() {
            var tts = new TypetalkClient(Credential.ClientId, Credential.Secrets);
            var res = tts.Search.Friends().Result;
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Accounts.Count() > 0);
        }
    }
}
