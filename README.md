# Steins;Gate Translator
Steins Gate Translator is an easy-to-use executable software that allows you to translate the game into any language via English. The game is required to use the files.

<img width="1080" height="512" alt="Sans titre-1" src="https://github.com/user-attachments/assets/77b65c00-f325-42bf-a7ec-210f2a85569c" />

# Steins Gate Translator - Translation Guide

## 1. Retrieve the Original MPK File
- Go to the `Steins Gate` folder and copy the `script.mpk` file into the `dialogues/MPK original` folder.

## 2. Extract MPK Contents
- Launch `SGOMPK.exe` located at the root of the `Steins Gate Translator` folder.
- Click **UnPack and List**.
  - In **MPK file**, select your original file.
  - In **Save**, choose the `dialogues/scx original` folder.
- Click **UnPack**.

## 3. Convert SCX Files to TXT
- Open the `dialogues` folder and run the `SCX to TXT.bat` file.
  - A terminal will open briefly and then close automatically.

## 4. Translation
- Launch **Steins Gate Translator** to perform your translation.

## 5. Compile and Create the Translated MPK
- Once the translation is complete, click the **Compilation** button.
- To recreate an MPK file:
  - Launch `SGOMPK.exe` in **RePack** mode.
  - Ensure the `scx original` folder is inside **Directory Data**.
  - In **Save**, select the `dialogues/MPK translate` folder.
  - Name the file `script.mpk` (very important).
  - Click **RePack**.
- Replace the `script.mpk` file in the Steins Gate game folder.

## 6. Finalization
- Your translation is ready. Enjoy the game!  
- *El Psy Kongroo.*

## 7. Useful Information and Warnings
- Please maintain the line breaks in the `.txt` files to avoid any issues after compilation.
- Avoid using special characters such as "é, à, ô, û, ..." as they may cause the game to crash.
- Opening a `.txt` file may take some time because the software highlights all tags in bold to make translation easier.
- The software may receive updates. When using the latest version, simply move all your `scx` and `txt` folders to the new version.

## 8. Credits and Sources
- sg-unpack: https://github.com/rdavisau/sg-unpack (For SCX to TXT decompilation and vice versa)
- SGOMPK MagesPack: https://github.com/DanOl98/MagesPack (For MPK to SCX decompilation and vice versa)
