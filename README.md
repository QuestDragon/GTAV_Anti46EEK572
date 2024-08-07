# Anti46EEK572 - Created By QuestDragon
Version: 1.1.0
## 作成した経緯
RPHで動作する、Cheep氏の「[MoreLicensePlates (No More 46EEK572)](https://www.gta5-mods.com/scripts/morelicenceplates-rph-no-more-annoying-42eek572)」があることは知っていたのですが、RPHがないと動作しないため、
LSPDFR等をプレイしていないModプレイヤーにとってはScriptHookVやDotNetで動作するModでないと都合が悪いな…と思い、5modsなどのサイトで似たようなModを探してみたのですが、なかったので作ってみました。

なければ作ればいいじゃない。そういう精神です。

## 機能
至ってシンプルで、指定したナンバープレートの車両に乗車すると、スクリプトが自動でナンバープレートをランダムにしたり、別のナンバープレートに書き換えてくれます。

NPC車両に関しても、設定を有効にすることで同じようにナンバープレートを書き換えてくれます。

## 機能追加、フィードバックについて
制作者は初心者なので何かと至らないところがあると思います。

不具合等を発見しましたら、QuestDragonまでご連絡ください。

また、「こんな機能がほしい！」「ここはこうしてほしい！」という要望がありましたらご相談ください。

こちらもスクリプトModについて勉強したいので、ご意見や要望はいつでもお待ちしております。

## 開発環境
C#を使用しています。

ScriptHookV DotNetを使用しており、バージョンは3.6.0で開発しています。

## インストール
以下から各種ファイルをダウンロードし、スクリプトMod本体はScriptsフォルダに、前提条件のファイルはGTA5.exeと同じフォルダにコピーしてください。

| [Anti46EEK572](https://github.com/QuestDragon/GTAV_Anti46EEK572/releases/latest/download/Anti46EEK572.zip) | [ScriptHookV](http://dev-c.com/gtav/scripthookv/) | [ScriptHookV DotNet 3.6.0](https://github.com/scripthookvdotnet/scripthookvdotnet/releases/latest) |
| ------------- | ------------- | ------------- | 

## 各種設定
### 動作設定
スクリプトの動作設定はiniファイルから行います。

基本的にiniファイル内にも説明を記述しているので困ることはないと思いますが、こちらにも記載しておきます。

**PlayerVehicle**：プレイヤーが車両に乗り込んだ際に、スクリプトが動作するかを指定します。`True`で有効、`False`で無効です。これ以外の指定を行うと`False`として扱われます。

**NPCVehicles**：NPC車両全てに対し、スクリプトが動作するかを指定します。`True`で有効、`False`で無効です。これ以外の指定を行うと`False`として扱われます。

### ナンバープレート指定
ナンバープレートの指定はxmlファイルから行います。

xml内に軽く説明を載せておりますので、そちらも併せてご確認ください。

タグは必ず用意してください。抜けがあったりすると正常に動作しない可能性があります。

### 各タグの説明
**AntiPlates**：このタグの中に設定内容を書き加えていきます。ここは変更しないでください。

**PlateSetup**：各プレートごとの設定を記述するためのタグです。設定項目ごとに用意します。

**Plates**：ナンバープレートを指定するタグを格納するタグです。

**Overrides**：ナンバープレートの変更内容を指定するタグを格納するタグです。

**Plate**：ナンバープレートを指定するタグです。

**Override**：ナンバープレートの変更内容を指定するタグです。

### 書き方

`<Plate>`タグの中に、変更対象のナンバープレートを指定してください。

`<Override>`タグの中には、変更したいナンバープレートを指定してください。ランダムにナンバープレートを生成してほしい場合は空タグ（`<Override />` と入力）にしてください。

つまり、`<Plate>`タグの中に指定したナンバープレートがついた車両に乗り込むと、`<Override>`タグに指定したナンバープレートにスクリプトModが書き換えてくれる、ということになります。

### 複数指定する方法
`<Plate>`タグと`<Override>`タグは複数指定が可能で、指定したいナンバープレートの数だけタグを増やしていただければ、該当する`<Plate>`タグのナンバープレート車両に乗車した際に、ランダムで`<Override>`タグのナンバープレートを選んで書き換えてくれます。

*※1つのタグの中に複数のナンバープレートを書くのではなく、ナンバープレートの数だけタグを用意して1つずつ指定するので、お間違えのないようにご注意ください。*

#### 正しい例

```
<Plate>46EEK572</Plate>
<Plate>5MDS003 </Plate> ← 8文字にしないといけないので半角スペースで不足分を補完
<Plate> FC1988 </Plate> ← 8文字にしないといけないので半角スペースで不足分を補完
<Plate>Betty 32</Plate>
```

#### 間違い例

```
<Plate>46EEK572, 5MDS003, FC1988, Betty 32</Plate>
```

### Plateタグ指定時の注意点

上記でも軽く触れてますが、ナンバープレートを指定するときの注意点として、【必ず8文字】で指定してください。

GTA5は仕様上、ナンバープレートの構成は8文字と決まっています。

ですが、Mod等を使用すれば、8文字以内でナンバープレートを書き換えることもできるようになっています。*（公式でもiFruitアプリでできますが）*

このとき、8文字より少ない場合は左右に半角スペースが自動的に入り、文字が中央揃えになるように調整されます。

そのため、XMLファイルに指定する際、文字だけを指定すると、スクリプトがナンバープレートの比較を行うことができません。

8文字より少なく指定する場合は、中央揃えになるよう、半角スペースを左右に追加して合計8文字になるよう調整して指定してください。

## 使い方

特にゲーム内で操作することはなく、車両に乗り込むだけでスクリプトModがXMLファイルとナンバープレートを照らし合わせて、指定されたナンバープレートであると判断した場合は自動で書き換えを実行してくれます。

NPC車両や駐車車両についても、iniファイルで有効化することで同じように動作します。

## 余談

指定するナンバープレートが8文字以内の際に、自動で中央揃えにする仕組みはもしかしたらアップデートでスクリプトMod側で対応させるかもしれません…。

即興で作ったスクリプトModなので、アップデートするかどうかは分かりませんが…。以外と気分屋なんですよね、自分…ｗ

## 免責事項
本スクリプトModを使用したことにより生じた被害に関して、私QuestDragonは一切の責任を負いかねます。自己責任でご使用ください。

ファイルに一切変更を加えない状態での2次配布は禁止です。

予告なく配布を停止することがあります。予めご了承ください。

改造はご自由にしていただいて構いませんが、配布の際はクレジット表記していただけると助かります。

「一から自作した」というのではなく、「QuestDragonのスクリプト(Anti46EEK572)の〇〇を△△にした」のように表記していただければと思います。

## 制作者
QuestDragon
