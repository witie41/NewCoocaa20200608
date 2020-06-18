using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllData
{
    //测试
    public static string DataString = "http://beta-vr.coocaa.com";
    public static int userId = -1;
    public static string token = "";
    public static string UserName = "未登录";
    public static GameObject Gifts;
    public static GameObject Emoji;
    public static ReportData[] reportDatas;
}


public class LivingRoomData
{
    public int id;
    public int userId;
    public string playUrl;
    public string score;
    public string title;
    public string announcement;
    public string foreshow;
    public string coverImg1;
    public int collectNum;
    public int attentionNum;
    public int broadcastRecordId;
    public string roomStatus;
    public string nickName;
    public string broadcastCategory;



}

public class VideoData
{
    public int workId;
    public int workType;
    public string classificationName;
    public string title;
    public string cover;
    public string labels;
    public string description;
    public long updateTime;
    public long releaseTime;
    public string nickName;
    public string path;
    public string md5;
    public int userId;
    public int likeNum;
    public int collectNum;
    public int playNum;
    public int videoType;
    public int duration;
}

public class SubscribeRoom
{
    public int broadcastCategoryId;
    public string broadcastCategoryName;
    public string broadcastTitle;
    public string coverImg;
    public int dataId;
    //明天改成string
    public int roomStatus;
    public int userId;
}


public class DanmuData
{
    public int id;
    public string message;
}

public class MsgRecieve
{
    public int masterId;
    public string msgType;
    public int broadcastRecordId;
    public long sendDate;
    public int broadcastId;
    public int id;
    public string title;
    public string msgStatus;
    public string content;
}


public class EmojiData
{
    public int id;
    public string title;
    public string path;
}

public class GiftData
{
    public int id;
    public string giftType;
    public string name;
    public string imgPath;
    public int coocaaCoin;
}

public class ReportData
{
    public string name;
    public int id;
    public int seq;
}

public class FanData
{
    public int dataId;
    public string headImage;
    public int userId;
    public string userName;
}

public class GuestData
{
    public int broadcastId;
    public string nickName;
    public int userId;
    public string headImage;
}

public class GiftRank
{
    public int acceptUserId;
    public int broadcastId;
    public int broadcastRecordId;
    public int giftId;
    public int giftNum;
    public int presentedUserId;
    public string presentedUserName;
    public int saplingCoin;
    public string headImage;
}

public class UserToken
{
    public string token;
}

public class VerificationImage
{
    public string codeKey;
    public string codelmg;
}

public class PopularityList //人气榜单
{
    public int collectNum;
    public string headImage;
    public int likeNum;
    public string nickName;
    public int playNum;
    public int userId;
}

public class UserData
{
    public int id;
    public string nickName;
    public string signature;
    public string headImage;
    public int worksNum;
    public int playNum;
    public int likeNum;
    public int myAttentionNum;
    public int attentionNum;
    public int collectNum;
}

public class UserDataFind
{
    public string access_token;
    public string headImage;
    public int id;
    public string name;
    public string nickName;
    public string openId;
    public string phone;
    public string signature;
    public long skyId;
    public int tokenTime;
    public string type;
}

public class App//游戏或者应用或者web
{
    public string apkIcon;
    public string apkId;
    public string apkName;
    public string apkPackage;
    public int contentId;
    public string contentType;
    public string webappLink;
}


public class Info
{
    public int code;
    public string data;
    public string msg;
}
public class FirstSelected
{
    public int contentId;
    public string contentType;
    public string description;
    public int index;
    public string recommendAction;
    public int recommendId;
    public int recommendStatus;
    public string recommendTitle;
    public string recommendType;
    public string thumbSize;
    public string thumbStyle;
    public string thumbUrl;
}

public class RecommendVideo
{
    public int contentId;
    public string contentType;
    public string resourceId;
    public string source;
    public string sourceName;
    public string url;
    public string videoTag;
    public string videoType;
}
public class RecommendLivng
{
    public string cover;
    public string resourceId;
    public string videoType;
    public int contentId;
    public string source;
    public string sourceName;
    public string contentType;
    public string url;
}

public class RecommendZhubo
{
    public int attentionNum;
    public int collectNum;
    public string headImage;
    public int id;
    public int likenum;
    public int myAttentionNum;
    public string nickname;
    public int playNum;
    public string signature;//签名
    public int worksNum;
}


