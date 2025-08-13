# Steins Gate Translator
Steins Gate Translator est logiciel executable simple à prendre en main pour pouvoir traduire le jeu dans n'importe qu'elle langue via l'anglais. Le jeu est requis pour pouvoir utiliser les fichiers.

# Steins Gate Translator - Guide de Traduction

## 1. Récupération du fichier MPK original
- Rendez-vous dans le dossier `Steins Gate` et copiez le fichier `script.mpk` dans le dossier `dialogues/MPK original`.

## 2. Extraction du contenu MPK
- Lancez `SGOMPK.exe` situé à la racine du dossier `Steins Gate Translator`.
- Cliquez sur **UnPack and List**.
  - Dans **MPK file**, sélectionnez votre fichier original.
  - Dans **Save**, indiquez le dossier `dialogues/scx original`.
- Cliquez sur **UnPack**.

## 3. Conversion des fichiers SCX en TXT
- Ouvrez le dossier `dialogues` et exécutez le fichier `SCX to TXT.bat`.
  - Un terminal s’ouvrira brièvement puis se fermera automatiquement.

## 4. Traduction
- Lancez **Steins Gate Translator** pour effectuer votre traduction.

## 5. Compilation et création du MPK traduit
- Une fois la traduction terminée, cliquez sur le bouton **Compilation**.
- Pour recréer un fichier MPK :
  - Lancez `SGOMPK.exe` en mode **RePack**.
  - Assurez-vous que le dossier `scx original` se trouve dans **Directory Data**.
  - Dans **Save**, choisissez le dossier `dialogues/MPK translate`.
  - Nommez le fichier `script.mpk` (très important).
  - Cliquez sur **RePack**.
- Remplacez le fichier `script.mpk` dans le dossier du jeu **Steins Gate**.

## 6. Finalisation
- Votre traduction est prête. Bon jeu !  
- *El Psy Kongroo.*

## 7. Remerciement et sources
- sg-unpack : https://github.com/rdavisau/sg-unpack
- SGOMPK MagesPack : https://github.com/DanOl98/MagesPack
