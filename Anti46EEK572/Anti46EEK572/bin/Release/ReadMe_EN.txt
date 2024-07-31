Anti46EEK572 - v1.0.0 (Created By QuestDragon)
*This explanation uses Google Translate.

~Background to creation~
I knew there was a "MoreLicensePlates (No More 46EEK572)" by Cheep that worked with RPH, but it had a drawback: it didn't work without RPH.
I thought that it would be inconvenient for mod players who don't play LSPDFR etc. if the mod didn't work with ScriptHookV or DotNet, so I looked for a similar mod on sites like 5mods, but couldn't find one, so I made one.
If it doesn't exist, just make it. That's the spirit.

~Function~
It's very simple, and when you get into a vehicle with a specified license plate, the script automatically randomizes the license plate or rewrites it to a different license plate.

~About adding features and feedback~
The creator is a beginner, so I'm sure there are some shortcomings.
If you find any problems, please contact QuestDragon.
Also, if you have any requests such as "I want this function!" or "I want this part to be like this!", please contact us.
I would also like to learn more about script mods, so I'm always looking forward to your opinions and requests.

~Included items~
The zip file contains the following files.
scripts folder: This folder contains the script mod and its associated files.
	Anti46EEK572.dll: This is the script itself.
	Anti46EEK572.xml: This is the configuration file.
Readme_JP.txt: This is the Japanese manual. (This file)
Readme_EN.txt: This is the English manual.

~ Installation ~
Copy the scripts folder to the Grand Theft Auto V folder.
Download Script Hook V from the link below, unzip the zip file, and copy the two dll files in the bin folder to the Grand Theft Auto V folder.
http://dev-c.com/gtav/scripthookv/
Download Script Hook V .NET from the link below, unzip the zip file, and copy the file with the string "ScriptHookVDotNet" in it to the Grand Theft Auto V folder.
https://github.com/scripthookvdotnet/scripthookvdotnet/releases/latest
In addition, the above two items must meet the requirements for operation. Please check the description on each download page for details.

~Various Settings~
Settings are done from the xml file.
There is a brief explanation in the xml, so please check that as well.
Be sure to prepare the tags. If any are missing, it may not work properly.
Add the settings inside the <AntiPlates> tag. Prepare a tag called <PlateSetup> below this tag. This is the setting for each plate.
In the <PlateSetup> tag, prepare a <Plates> tag and an <Overrides> tag.
In the <Plates> tag, prepare a <Plate> tag, and in the <Overrides> tag, prepare an <Override> tag.
In the <Plate> tag, specify the license plate to be changed.
In the <Override> tag, specify the license plate you want to change to.
In other words, when you get into a vehicle with a license plate specified in the <Plate> tag, the script mod will change it to the license plate specified in the <Override> tag.
You can specify multiple <Plate> and <Override> tags. If you add as many tags as you want to specify license plates, the license plate in the <Override> tag will be selected at random when you get into a vehicle with the license plate in the <Plate> tag.

*Please note that you do not write multiple license plates in one tag, but prepare tags for the number of license plates and specify them one by one, so please be careful not to make a mistake.

Correct:

<Plate>46EEK572</Plate>
<Plate>5MDS003</Plate>
<Plate> FC1988 </Plate> ← It must be 8 characters, so fill in the missing parts with half-width spaces
<Plate>Betty 32</Plate>

Incorrect:

<Plate>46EEK572, 5MDS003, FC1988, Betty 32</Plate>

As mentioned briefly above, when specifying a license plate, be sure to specify [8 characters].
In GTA5, the number plate is limited to 8 characters.
However, by using mods, it is possible to rewrite the number plate to 8 characters or less. (This can also be done officially with the iFruit app.)
In this case, if there are fewer than 8 characters, half-width spaces are automatically inserted on the left and right, and the characters are adjusted so that they are centered.
Therefore, when specifying the XML file, if you specify only characters, the script will not be able to compare the number plate.
If you specify fewer than 8 characters, add half-width spaces on the left and right to adjust the total to 8 characters so that they are centered.

How to use
There is no need to operate in the game, just get in the vehicle and the script mod will compare the XML file with the number plate, and if it determines that it is the specified number plate, it will automatically rewrite it.
In addition, since the XML file is loaded each time, if you add or edit it on the spot, the changes can be reflected without reloading the game or script.

A side note
The mechanism for automatically centering the number plate when the specified number plate is 8 characters or less may be supported by the script mod in an update...
This is a script mod I made on the spot, so I don't know if I'll update it or not... I'm surprisingly moody, lol

~Disclaimer~
I, QuestDragon, take no responsibility for any damage caused by using this script mod. Use at your own risk.
Secondary distribution is prohibited.
Distribution may be stopped without notice. Thank you for your understanding.

Produced by: QuestDragon