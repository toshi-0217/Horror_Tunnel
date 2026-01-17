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
- **エディタ** : Visual Studio Code
- **3Dモデル・テクスチャ** : Blender4.4(自作Prehab)、一部AssetStoreのものを利用（Terrain用テクスチャ・木のPrehab等）
- **Platform** : PC(Windows)
- **音声** : VOICE VOX、その他フリー音源サイトから利用(使用させていただいた素材のサイトは最後に記載しています)

※Gitによるソース管理は行いませんでした。

## 3．ゲームシステム
### 3.1 Scene
Sceneは「start」,「maingame」,「clear」の3つで構成されています。すべてのSceneは、Asset/Scenesに置いてあります。
#### 3.1.1 startシーン
ゲームのスタート画面となるシーンです。Terrainとprobuilderを用いて背景を作りました。startボタンを押すとmaingameへ遷移します。

<img src="./readmesource/gif/titlemenu.gif" width="50%"/>

#### 3.1.2 maingameシーン
実際にプレイするゲーム画面のシーンです。薄暗いトンネル内をプレイヤーは脱出を目指して徘徊します。さまざまなホラー演出が不定期で出現します。演出に関しては3.2で紹介します。トンネルの出口へ到達するとclearへ遷移します。

<img src="./readmesource/gif/start.gif" width="50%"/>

#### 3.1.3 clearシーン
clearメッセージを表示させるシーンです。表示が終わると自動的にstartシーンへと遷移します。

<img src="./readmesource/gif/goal.gif" width="50%"/>

### 3.2 トンネル生成

トンネルの長さはランダムで決まります(何セグメントつなげるかを決める)。具体的には、トンネルの1セグメント分のPrehabをインスタンス化してつなげることで実装しています。きれいにつなげるために、ひとつ前のセグメントの終わりのｚ座標を取得しておき、その座標に合わせて次のセグメントをインスタンス化します。また、セグメントは通常、ホラー演出1、ホラー演出2、ゴールの4種類あり、ゴールはトンネルの最後に必ず生成されます。ホラー演出用のセグメントは、初期確率として、1％の確率でどちらかが生成されます。後述するホラー演出と同様、確率は抽選が外れる度に上昇していきます。

トンネル生成のスクリプト: Asset/scripts/Object_Instance/Tunnel_Generate.cs

### 3.3 プレイヤー操作

プレイヤー操作にはUnityのInput Actionを利用し、キーボードとマウス(WASDで移動、マウスポインタで視点移動)での操作方法を実装しました。

特別な操作方法はないため、あまりゲームに触れない人もプレイしやすいと思います。

Input Actionは複数の入力デバイスでの操作を統一的に入力管理システムで、現在のpcゲームではキーボード・マウスだけでなくコントローラー操作にも対応できたり、スマートフォンとのクロスプレイも対応しているものも多いため複数入力デバイス操作の統一管理はゲーム開発において必須の技術だと考え、Input Actionを利用しました。

プレイヤー操作のスクリプト: Asset/scripts/Player/playermove.cs

### 3.4 ホラー演出
ホラー演出に関するスクリプトは、Asset/scripts/jumpscareに置いてあります。

また、実装に使った効果音などは、Asset/Audioに置いてあります。

全ての演出はそれぞれが持つ確率で発生します。抽選して発生しなかった場合、次の抽選が行うまでのインターバルもそれぞれ固有の秒数を持っています。また、ホラーゲームとして飽きにくくなるような工夫として、抽選が外れると発生確率が上昇し、抽選が当たり演出が発生すると確率が0に下がります。そのため、すべての演出が発生しやすく、同じ演出が連続で発生しにくくなるように実装しました。

#### J-01:停電
トンネル内の照明が突然すべて停電します。暫くすると復旧します。

実装にはオブザーバーパターンという設計を利用しています。司令塔となるスクリプト(LightManager.cs)と実行役となるスクリプト(LightController.cs)の二つで構成されています。トンネルセグメントがインスタンス化された時、そのトンネル内にある照明のオブジェクトにアタッチされているスクリプトのメソッドをイベントとして登録しておき、.invoke()を発動することでイベントを一斉に実行する形で実装しました。

script: Assets/scripts/jumpscare/Light内の複数の.csファイル

<img src="./readmesource/gif/Lightdown.gif" width="50%"/>

#### J-02:ノック音

非常ドアからドアをたたく音が聞こえてきます。ドアとプレイヤーが一定距離近づくとノック音が鳴る仕組みとなっています。音源が4種類あり、ランダムな順で鳴るようになっています。

確率管理にはScriptable Objectを用いてインスタンス元のオブジェクトのパラメータを操作することで確率の増減を実装しました。

※音がメインのため、gifではなく動画形式です。

https://github.com/user-attachments/assets/7c875604-03a6-417d-9b99-a2841a2fd459

script: Assets/scripts/jumpscare/Door/DoorKnock.cs

Scriptable Object: Assets/scripts/system/scriptable object/door_occur

#### J-03:叫び声

トンネル全体に響き渡る叫び声が聞こえてきます。

※音がメインのため、gifではなく動画形式です。

https://github.com/user-attachments/assets/789a4994-afa4-4e34-be54-0a4ed8337af4

script: Assets/scripts/jumpscare/shout/sakebigoe.cs

#### J-04:消火器

消火器が死角から突然倒れてきます。

消火器のプレハブに当たり判定となるcolliderの子オブジェクトを付けて、プレイヤーが当たり判定に触れることで乱数生成を行い、消火器を倒すかどうかを決めています。

ドアのノック音と同様、確率管理にはScriptable Objectを用いてインスタンス元のオブジェクトのパラメータを操作することで確率の増減を実装しました。

script: Assets/scripts/jumpscare/syokaki/syokakimove.cs

Scriptable Object: Assets/scripts/system/scriptable object/syokaki_occur

<img src="./readmesource/gif/syokaki.gif" width="50%"/>

#### J-05:工事中

道路工事の標識とカラーコーンでトンネルの右側しか通れなくなっています。右側を通って進もうとすると、工事標識が倒れてきます。

発生原理は消火器と同様colliderを使っていますが、トンネル生成の時で標識とカラーコーンのオブジェクトを生成するかどうかを抽選して決めるため、消火器とは違いこれらのプレハブがインスタンス化された時点で演出の発生が確定します。

script: Assets/scripts/jumpscare/plate_crush/SighFall.cs

<img src="./readmesource/gif/platebreak.gif" width="50%"/>

#### J-06:非常アナウンス

突然緊急放送が流れてきます。内容は、「トンネル内に子供が迷い込んでいるため、見つけ次第処分してほしい」というものです。

アナウンスの音声は女性で陰鬱な声質のものにしたいと考え探していた結果、VOICE VOXの「九州そら」というキャラクターのものを利用しました。

実装は単純な確率抽選で行っています。コルーチンの待機時間が他の演出と被らないようにしました。

※動画サイズが大きいため載せることができませんでした。

script: Assets/scripts/jumpscare/anaunce/anaunce_01.cs

#### J-07:ノイズ

画面全体に壊れたテレビのようなノイズが発生します。一定時間経過すると収まります。

実装に関しては、URPに標準搭載されているポストプロセスのGlobal Volumeを使用し、ノイズ、画面フィルターをスクリプトで制御しました。今までVolumeをあまり使ったことがなかったのですが、使ってみるととても便利だったため、これを機に今回使わなかった他の要素も利用したいと思いました。

script: Assets/scripts/jumpscare/noise/sandstorm.cs

<img src="./readmesource/gif/noise.gif" width="50%"/>

#### J-08:赤いノイズ

画面全体が赤くなり、ノイズのようなものと不気味な笑い声が響き渡ります。一定時間経過すると収まりますが、最後に謎の存在から「逃がさない」とささやかれます。

通常のノイズと同様、Global Volumeを利用しました。また、ノイズテクスチャは自身で作成した男性の3Dモデルの顔面の画像を使用しています。心霊的な不可解さを通常のノイズよりも出したいと思い、砂嵐ではなく怖い画像を取り入れました。

script: Assets/scripts/jumpscare/noise/sandstorm.cs

<img src="./readmesource/gif/rednoise.gif" width="50%"/>

#### J-09:泣く人形

歩いていると突然前方に鳴き声を発する日本人形が出現します。一定距離近づくと画面全体にジャンプスケアが発生します。

人形の手前にcolliderの当たり判定を設置して、プレイヤが触れることで発生します。ジャンプスケアは、非アクティブのCanvasをスクリプトでアクティブにすることで画面全体に日本人形の怖い画像を表示させています。

script: Assets/scripts/jumpscare/Doll内の複数.csファイル

<img src="./readmesource/gif/doll.gif" width="50%"/>

#### J-10:横たわる男

大量の医療用ベッドや車いすと、ベッドに横たわるスーツ姿の男性が出現します。男性に近づくとうめき声を発します。

車いす・医療用ベッドはAsset Storeのものを利用しています。人形等と同様、colliderの当たり判定を用いています。

script: Assets/scripts/jumpscare/sararyman/sararyman_voice.cs

<img src="./readmesource/gif/man.gif" width="50%"/>

#### J-11:幽霊

歩いていると突如ノイズと共に前方に長髪の女性のようななにかが出現します。一定時間近づくと消え、「だーれだ」とささやいてきます。

Global Volumeでノイズ、画面フィルター、レンズエフェクト、発光をスクリプトで制御しました。また、発生時にプレイヤーの操作をできないようにして、前方に幽霊のPrehabとプレイヤーがお互いを見つめあうように実装しました。

script: Assets/scripts/jumpscare/Ghost/GhostComing.cs

<img src="./readmesource/gif/Ghost.gif" width="50%"/>

## 4．Prehab
Prehabはblender4.4を用いて自身で作成したものと、AssetStoreから無料のものをダウンロードしたものを利用しています。

自身で作成したPrehabは、Asset/My assetsの中に置いてあります。以下のものを作成しました。
- 非常ドア
- 非常電源装置
- 非常警報器
- 消火器
- 工事標識
- 交通規制用のコーンとポール
- 換気ファン
- 日本人形
- スーツを着た男性
- 女性の幽霊
  
ダウンロードしたAssetは、Asset/import assetsの中に置いてあります。利用したAssetは以下の通りです(一部利用)。
- Street Lights Pack 01
- Asset pack for horror game
- Kajaman's Roads - Free
- Grass Flowers Pack Free
- Conifers [BOTD]
- Terrain Textures Pack Free

## 5．使用したフリー素材サイト

使用させていただいたフリー素材サイトは以下の通りです。

### 音声サイト

- [ぴたちー素材館](http://www.vita-chi.net/sec/voi/hora/voivoi1.htm)
- [On-Jin ～音人～](https://on-jin.com/kiyaku.php)
- [効果音工房](https://umipla.com/horror)
- [VSQplus](https://vsq.co.jp/plus/)
- [効果音ラボ](https://soundeffect-lab.info/)
- [無料効果音で遊ぼう！](https://taira-komori.net/index.html)

### テクスチャ・画像

- [illust AC](https://www.ac-illust.com/main/detail.php?id=2081242&word=%E3%83%8E%E3%82%A4%E3%82%BA#goog_rewarded)
- [BEIZ images](https://www.beiz.jp/%E7%B4%A0%E6%9D%90/%E5%B2%A9/00133.html)
- [Photo-AC](https://www.photo-ac.com/)
- [いらすと屋](https://www.irasutoya.com/)

### フォント

- [日本語フリーフォント](https://ffont.jp/)
- [Font Free](https://fontfree.me/) 
