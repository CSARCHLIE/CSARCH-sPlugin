# 🧩 CSARCH’s Plugin（SCP:SL EXILED 插件）

一个基于 EXILED 框架 的 SCP:SL 扩展插件集合，包含多个服务器玩法增强系统。

---

## ⚙️ 插件概述

- 🔫 无限弹药系统
- 🗝️ 钥匙卡权限重构
- 🧠 SCP机制扩展与修复
- 🧹 服务器清理优化
- 🧟 SCP特殊行为调整

---

## 📁 项目结构

```
Config.cs
Plugin.cs
API/
 ├── Commands/
 ├── Enums/
 ├── UI/
 └── FileName.cs
Events/
 ├── EventArgs/
 │   ├── Items/
 │   ├── Player/
 │   ├── SCP/
 │   └── Server/
 └── Handlers/
```

---

## 🚀 功能模块

### 🔫 Infinite Ammo
- 回合开始处理弹药逻辑
- 遍历 AmmoPickup
- 支持无限弹药机制

### 🗝️ KeycardSystem
- 背包钥匙卡检测
- 多权限判断
- 修复原版门逻辑问题

### 🧹 Cleaner
- 清理掉落物
- 优化服务器性能

### 🧟 SCP 相关
- SCP-207 行为修复
- SCP-3114 行为扩展
- SCP 连续 30 秒未受伤且原地静止后，每 0.2 秒恢复 1 点生命值

### 👤 玩家功能
- 为 D 级人员发放钥匙卡
- 将 MTF 列兵的特工卡替换为列兵卡
- 显示玩家名称 UI

---

## 🧠 入口

### Plugin.cs
- 注册 EXILED 事件
- 定义插件信息

### Config.cs

```csharp
public bool Debug { get; set; } = false;
public bool IsEnabled { get; set; } = true;
```

---

## 📌 环境

- SCP:SL
- EXILED
- .NET Framework 4.8.1
- C# 7.3

---

## 🚀 安装

1. 编译 DLL
2. 放入 EXILED Plugins 目录
3. 启动服务器

---

## ⚠️ 注意

- 无限弹药影响平衡
- KeycardSystem 会改变原版门逻辑
- 建议测试服先用
