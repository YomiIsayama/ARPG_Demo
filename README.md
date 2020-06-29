# 游戏数值分析及程序实现部分


### a) 人物管理ActorManager
1. 角色控制ActorController，RootMotionControl
1. 视角控制CameraController
1. 用户输入控制IUserInput,KeyboardInput

### b)	战斗管理BattleManager
1. 目标管理TargetManager
1. 武器管理WeaponManager
1. 武器控制WeaponController
1. 武器数据WeaponData
1. 巡逻->追逐->攻击Enemy_trigger，DummyIUserInput

### c)	状态管理StateManager
1. 事件管理EventCasterManager
1. 地面碰撞OnGroundSensor
1. FMS处理动画事件FSMClearsignals，FSMOnEnter，FSMOnExit，FSMOnUpdate
1. 动画事件TriggerControl

### d)	互动管理InteractionManager
1. Timeline管理DirectorManage
1. 存档系统 GameSaveManager（ScriptableObject实现，该项目内存档只对背包物品位置，数量，生效，并不是存档最佳选择。）

### e)	UI管理UIManager
1. 对话系统 FungusSetting
1. 战术模式 ButtonEvent
1. 背包系统 Inventory，InventoryManager，Item，ItemOnDrag，MoveBag ，ItemOnWorld，Slot
### f)	系统管理GameManager（因为某些原因，所以空着了，交给了各个manager吧）
1. 单例基类 Single
1. 继承MonoBehaviour的单例基类 SingleMono
1. Ab包测试 CreateAssetBundles

# 游戏说明
### g)	插件使用
1. 对话插件Fungus；
1. 动画处理插件DOTween；
1. Timeline配合使用 DefaultPlayables，处理timeline操作事件所需功能；
### h)	开源使用
1. FF7RE战术模式参考 https://github.com/mixandjam/FFVII-TacticalMode
1. 参考大佬的开源代码，并融入自己开发中使用。在UIManager ，TargetManager，ButtonEvent中做出了较多的修改。
1. 我修改了ButtonEvent与UI中的逻辑代码，通过代码控制检测选着哪个skill而选择目标，作出正确的反馈，日后可以随意修改，并添加任意skill方法到TargetManager；
1. 修改了战术模式内targetCam与main camera的问题；
1. 修改了targets[targetIndex]在敌人死亡后 miss的问题；
1. 修改了原有部分DOTween在敌人会运动时出现的问题；

### i)	素材使用
1. Player角色与动画 使用asset store 中的Dynamic Sword Animset包，Enemys使用mixaom中的Ybot模型与SwordAndShield动作包。
1. UI相关主要使用asset store 中的 Simple Health Bar，Simple Fantasy GUI，SimpleDungeons等包。


## 游戏叙述
1. Only some parts of gameplay of ARPG
## 游戏的玩法介绍
1. WASD ：上下左右；
1. Left shift ：切换奔跑/行走 状态；
1. 鼠标左键 ：普通攻击；
1. 鼠标右键 ：举盾防御；
1. 鼠标移动 ：视角移动；
1. Tab ：锁定；
1. F ：处决/对话/触发事件；
1. 空格 ：跳跃；
1. Left alt ：翻滚；
1. Left ctrl ：冲刺；
1. Q ：弹反；
1. ESC ：菜单；
1. I ：背包；
1. T : 仿FF7RE 战术模式（ATB蓝条，受伤/攻击 一次获得25%，使用一次skill消耗50%）；
1. 在战术模式中，会自动锁定一定范围内的敌人，手动选择，切换目标，进行攻击/技能/回血


