using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

//https://www.shibuya24.info/entry/timeline_basis
//配备并使用了PlayableBehaviour类和PlayableAsset类的子类。
//PlayableBehaviour类定义了PlayableTrack在特定时机下发生的事件函数，如下所示
//OnGraphStart ……TimeLine开始时执行
//OnGraphStop……TimeLine结束时执行
//OnBehaviourPlay ……PlayableTrack播放时执行
//OnBhaviourPause……PlayableTrack停止时执行
//prepareFrame……PlayableTrack播放时每帧都执行


[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public ActorManager am;
    public float myFloat;
    //PlayableDirector pd;

    public override void OnPlayableCreate(Playable playable)
    {
    }

    public override void OnGraphStart(Playable playable)
    {
        //pd = (PlayableDirector)playable.GetGraph().GetResolver();


        // //取出物件 調用 am.xxxx 方法  
        // pd = (PlayableDirector) playable.GetGraph().GetResolver();

        // foreach (var track in pd.playableAsset.outputs)
        // {
        //     //鎖死狀態機  am.LockUnLockActorController(true);
        //     if (track.streamName == "Attacker Script" || track.streamName == "Victim Script")
        //     {
        //         ActorManager am = (ActorManager) pd.GetGenericBinding(track.sourceObject);
        //         am.LockUnLockActorController(true);
        //     }
        // }

    }

    public override void OnGraphStop(Playable playable)
    {


        // if( pd != null)
        //     {
        //         pd.playableAsset = null;
        //     }


        // foreach (var track in pd.playableAsset.outputs)
        // {
        //     //解鎖狀態機  am.LockUnLockActorController(false);
        //     if (track.streamName == "Attacker Script" || track.streamName == "Victim Script")
        //     {
        //         ActorManager am = (ActorManager) pd.GetGenericBinding(track.sourceObject);
        //         am.LockUnLockActorController(false);
        //     }
        // }



    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        am.LockUnLockActorController(true);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        am.LockUnLockActorController(false);
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        am.LockUnLockActorController(true);
    }
}