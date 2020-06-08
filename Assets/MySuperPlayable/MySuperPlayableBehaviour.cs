using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

//https://www.shibuya24.info/entry/timeline_basis
//�䱸��ʹ����PlayableBehaviour���PlayableAsset������ࡣ
//PlayableBehaviour�ඨ����PlayableTrack���ض�ʱ���·������¼�������������ʾ
//OnGraphStart ����TimeLine��ʼʱִ��
//OnGraphStop����TimeLine����ʱִ��
//OnBehaviourPlay ����PlayableTrack����ʱִ��
//OnBhaviourPause����PlayableTrackֹͣʱִ��
//prepareFrame����PlayableTrack����ʱÿ֡��ִ��


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


        // //ȡ����� �{�� am.xxxx ����  
        // pd = (PlayableDirector) playable.GetGraph().GetResolver();

        // foreach (var track in pd.playableAsset.outputs)
        // {
        //     //�i����B�C  am.LockUnLockActorController(true);
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
        //     //���i��B�C  am.LockUnLockActorController(false);
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