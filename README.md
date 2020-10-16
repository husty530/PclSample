# PclSample
  
--- 
  
# Contents
  
[Form.cs](/Form.cs) ... UIの部分。  
[PclWrapper.cs](/PclWrapper.cs) ... C++版PointCloudLibraryのラッパークラス。  
肝心のC++のDLLは別ソリューションで作りました。環境依存が強すぎるので共有できません。作り方は下記"How to Use"を参照ください。  
  
---  
  
# Preparation
  
* Windows10  
* VisualStudio ... C++デスクトップ開発、.NETデスクトップ開発  
  
---  
  
# How to Use
  
## C++側
  
基本的には[コチラ](http://tecsingularity.com/pcl/environment/)にあるとおり環境構築を行います。  
ただしその記事ではバージョンが古いので、PCL本体は[ココ](https://github.com/PointCloudLibrary/pcl/releases)から最新版を、途中に出てくる"main.cpp"と"CMakeList.txt"はこのリポジトリに同梱しているものを使っていただければ大丈夫です。  
書いているとおりにCMakeでソリューションを生成できたら、"project"の名前を"PCL"に、プロパティの"全般"、"詳細"で"プロジェクト名"、"ターゲット名"も"PCL"に、".exe"となっている2か所を".dll"に書き換えます。  
さらに、プロパティの"全般"→"出力ディレクトリ"にC#の.exeができるディレクトリを書いておくとそこにDLLが生成されます。  
  
かなり雑な説明でしたので不明点あればお知らせください。  
  
## C#側
  
Pathが通っていてDLLさえできていれば、C#側の"デバッグ"ですぐに実行できます。  
うまくいけば以下のようになります。  
  
![pcl1.png](/pcl1.png)  
  
Customの方では自分で値を変えてランダムな描画ができます。  
なんか格好いい幾何模様を描いてください。自分は数学に弱いので何も描けませんでした。。。  
  
![pcl2.png](/pcl2.png)
