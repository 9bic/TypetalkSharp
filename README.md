# TypetalkSharp - Typetalk API Client for CSharp

TypetalkSharp is a client library of [Typetalk API](https://developer.nulab-inc.com/docs/typetalk) for .NET 4.5.1~  

## Examples

Get Messages with TypetalkSharp

``` C#
using TypetalkSharp;

var typetalk = new TypetalkClient("CLIENT_ID", "CLIENT_SECRET");
var topicId = await typetalk.Topics.All().First(x => x.Name == "TARGET_TOPICS").Topic.Id;
var messages = typetalk.Topics.Messages.Get(topicId);
```

Post message with TypetalkSharp

``` C#
using TypetalkSharp;

var typetalk = new TypetalkClient("CLIENT_ID", "CLIENT_SECRET");
var topics = await typetalk.Topics.All();
var result = typetalk.Topics.Messages.Post(
					topics.First(x => x.Name == "TARGET_TOPICS").Topic.Id,
					new NewMessage("message from TypetalkSharp!"));
```


## Bot Examples
TypetalkSharp has features for Typetalk Bot
API Access From TypetalkBot Class are limited

``` C#
using TypetalkSharp.Bot;

var typetalkBot = new TypetalkBot("YOUR_TYPETALK_TOKEN");
var topicId = 0;
var result = await typetalk.Topics.Messages.Post(topicId, new NewMessage("from TypetalkSharp!"));
```