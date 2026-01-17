# 制作物紹介 #あのトンネル
終わりの見えないトンネルからの脱出を目指す、一人称3Dホラーゲームです。
![title](./readmesource/title.png)
## 1．制作したきっかけ
小学生のころプレイしていたゲーム「妖怪ウォッチ2」内に登場するやりこみ要素「えんえんトンネル」を一人称視点にしたらさらにおもしろいと思い（本作は3人称後方視点）、制作を始めました。ホラーゲーム全般が好きなため、オリジナルのような不可解さや異質さではなく、薄暗いトンネルの中、がっつりホラー演出が襲ってくる！のような要素を取り入れることにしました。

参考:[妖怪ウォッチ2](https://www.youkai-watch.jp/yw2/)　[えんえんトンネル(ピクシブ百科事典)](https://dic.pixiv.net/a/%E3%81%88%E3%82%93%E3%81%88%E3%82%93%E3%83%88%E3%83%B3%E3%83%8D%E3%83%AB#h2_1)
## 2．作品概要
- **製作期間** : 5か月
- **制作人数** : 一人（個人開発）
- **ターゲット層** : 自身と同年代（妖怪ウォッチシリーズを良くプレイしていた人）の人、ホラーゲームが好きな人、ゲームにおいて複雑な操作が苦手な人
## 3．使用技術
- **Engine** : Unity 6000.2.7f2
- **言語** : C#
- **Render Pipeline** : URP(Universal Render Pipeline)
- **3Dモデル・テクスチャ** : Blender4.4(自作Prehab)、一部AssetStoreのものを利用（Terrain用テクスチャ・木のPrehab等）
- **Platform** : PC(Windows)
## 3．Asset構成
### 3.1 Scene
Sceneは「start」,「maingame」,「clear」の3つで構成されています。
#### 3.1.1 startシーン
ゲームのスタート画面となるシーンです。Terrainとprobuilderを用いてトンネルと山を作り背景を作りました。startボタンを押すとmaingameへ遷移します。
#### 3.1.2 maingameシーン
実際にプレイするゲーム画面のシーンです。薄暗いトンネル内をプレイヤーは脱出を目指して徘徊します。さまざまなホラー演出が不定期で出現します。演出に関しては3.2で紹介します。トンネルの出口へ到達するとclearへ遷移します。
#### 3.1.3 clearシーン
clearメッセージを表示させるシーンです。表示が終わると自動的にstartシーンへと遷移します。
### 3.2 scripts (ホラー演出)
#### J-01:停電
トンネル内の照明が突然すべて停電します。暫くすると復旧します。
script: Assets/scripts/jumpscare/Light
#### J-02:ノック音
非常ドアからドアをたたく音が聞こえてきます。
script: Assets/scripts/jumpscare/Door
#### J-03:叫び声
トンネル全体に響き渡る叫び声が聞こえてきます。
script: Assets/scripts/jumpscare/shout
#### J-04:消火器
消火器が死角から突然倒れてきます。
script: Assets/scripts/jumpscare/syokaki
#### J-05:工事中
道路工事の標識とカラーコーンでトンネルの右側しか通れなくなっています。右側を通って進もうとすると、工事標識が倒れてきます。
script: Assets/scripts/jumpscare/plate_crush
#### J-06:非常アナウンス
突然緊急放送が聞こえてきます。内容は、「トンネル内に子供が迷い込んでいるため、見つけ次第処分してほしい」というものです。
script: Assets/scripts/jumpscare/anaunce
#### J-07:ノイズ
画面全体に壊れたテレビのようなノイズが発生します。一定時間経過すると収まります。
script: Assets/scripts/jumpscare/noise
#### J-08:赤いノイズ
画面全体が赤くなり、ノイズのようなものと不気味な笑い声が響き渡ります。一定時間経過すると収まりますが、最後に謎の存在から「逃がさない」とささやかれます。
script: Assets/scripts/jumpscare/noise
#### J-09:泣く人形
歩いていると突然前方に鳴き声を発する日本人形が出現します。一定距離近づくと画面全体にジャンプスケアが発生します。
script: Assets/scripts/jumpscare/Doll
#### J-10:横たわる男
大量の医療用ベッドや車いすと、ベッドに横たわるスーツ姿の男性が出現します。男性に近づくとうめき声を発します。
script: Assets/scripts/jumpscare/sararyman
#### J-11:幽霊
歩いていると突如ノイズと共に前方に長髪の女性のようななにかが出現します。一定時間近づくと消え、「だーれだ」とささやいてきます。
script: Assets/scripts/jumpscare/Ghost
