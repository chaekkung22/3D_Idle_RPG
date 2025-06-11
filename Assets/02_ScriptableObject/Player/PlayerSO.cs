using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerGroundData
{
    [field:SerializeField] [field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
    
    [field:Header("RunData")]
    [field:SerializeField] [field: Range(0f, 2f)] public float DetectSpeedModifier { get; private set; } = 1f;
    [field:SerializeField] [field: Range(0f, 2f)] public float ChaseSpeedModifier { get; private set; } = 1.2f;
}

[Serializable]
public class PlayerAttackData
{
    [field:SerializeField] public List<AttackInfoData> AttackInfoDatas { get; private set; }
    public int GetAttackInfoCount() {return AttackInfoDatas.Count;}
    public AttackInfoData GetAttackInfoData(int index) {return AttackInfoDatas[index];}
}

[Serializable]
public class AttackInfoData
{
    [field:SerializeField] public string AttackName { get; private set; }
    [field:SerializeField] public int ComboStateIndex { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field:SerializeField] [field: Range(0f, 1f)] public float ComboTransitionTime { get; private set; }
    [field:SerializeField] [field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
    [field:SerializeField] [field: Range(-10f, 10f)] public float Force { get; private set; }
    [field: SerializeField] public int Damage;

    [field: SerializeField]
    [field: Range(0f, 1f)]
    public float Dealing_Start_TransitionTime { get; private set; } = 0.25f;
    [field:SerializeField] [field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; } = 0.35f;
}

[Serializable]
public class PlayerDetectData
{
    [field: SerializeField] public float SearchDistance { get; private set; } = 4f;
    [field: SerializeField] public float SearchCooldownTime { get; private set; } = 2f;
    [field: SerializeField] public float TargetChasingRange { get; private set; } = 3f;
}
    


[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    [field :SerializeField] public PlayerAttackData AttackData  { get; private set; }
    [field: SerializeField] public PlayerDetectData DetectData { get; private set; }
}

