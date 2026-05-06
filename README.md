# bcsh1-sem-prace
# 🐭 Mice, Mine & Magic

> Tahová 2D fantasy hra pro dva hráče | Varianta (b) – Jednoduchá počítačová hra | BCSH1

---

## 📋 Základní informace

| | |
|---|---|
| **Předmět** | BCSH1 – Základy C# a .NET |
| **Student** | Martin Pešek |
| **Platforma** | Unity (C#) |
| **Herní mód** | 2 hráči na jednom PC (hotseat) |

---

## 🎮 Popis hry

Tahová 2D fantasy hra pro dva hráče na jednom PC. Každý hráč ovládá myš na destruktibilním terénu. Střídavě využívají magické schopnosti a zbraně, aby eliminovali soupeřovu myš.

---

## 🖼️ Vizuál

vlastní vizuál postav, kreslený v grafickém programu Krita
animace:
<img width="2760" height="1504" alt="mys animace" src="https://github.com/user-attachments/assets/7d4506e9-40b9-408c-b57e-8c83abeb982a" />


Mapy sestavené pomocí ai generovaného pozadí a Tile assetů
<img width="1476" height="851" alt="Snímek obrazovky 2026-05-06 102609" src="https://github.com/user-attachments/assets/0847fe9a-c350-453e-9fde-75d96b9fe17a" />

---

## ✅ Funkcionality

### Herní systém
- Tahový systém – hráči se střídají, každý tah má časový limit (30 sekund)
- Systém HP – každá myš má životy, při 0 HP umírá
- Pád myší po zničení terénu pod nimi
- Smrt pádem do voidu

### Terén
- Destruktibilní terén
- Kouzla a výbuchy odstraní jednotlivé tiles
- 3 různé mapy s fantasy tématikou – důl, les, hrad

### ⚔️ Zbraně & Kouzla

| Název | Typ | Popis |
|---|---|---|
| 🔥 **Ohnivá koule** | Kouzlo | Vystřelí hořící projektil s obloukem, výbuch při dopadu |
| ⚡ **Blesk** | Kouzlo | Přímý paprsek zasahující první překážku nebo myš + působí knockback |
| 🏹 **Luk a šíp** | Zbraň | Šíp s fyzikou oblouku, menší ale přesný damage |
| 💣 **Magická bomba** | Kouzlo | Talibán parodie |
| 🪝 **Lano (grappling hook)** | Pohyb | Vystřelí lano pro přesun na vzdálené místo |
| 👊 **Úder (melee)** | Zbraň | Myš praští do země a zničí terén pod sebou |
| 🌀 **Teleportace** | Kouzlo | Okamžitý přesun myši na kliknuté místo |

### Fyzika
- Fyzika projektilů – gravitace, oblouk letu (Unity Rigidbody2D)
- Poloměr výbuchu – poškození myší dle vzdálenosti
- Knockback – odhození myší silou výbuchu nebo melee útoku
- Pohyb myší – chůze, skok, lano, teleportace

### Ukládání dat
- Ukládání výsledků zápasů a high score (JSON)
- Načítání map z PNG souborů

### UI
- Hlavní menu – nová hra, výběr mapy, quit
- HUD – životy myší, aktuální zbraň/kouzlo, časovač tahu
- End screen – zobrazení výherce + statistik

---

## 🛠️ Technické informace

| | |
|---|---|
| **Jazyk** | C# (.NET) |
| **Engine** | Unity |
| **Perzistence dat** | JSON soubory |
| **Fyzika** | Unity Rigidbody2D |
| **Destrukce terénu** | Texture2D pixel destruction |

---

## 🎨 Použité assety

### Vlastní assety
| Asset | Autor |
|---|---|
| Grafika myší a animace | autor: Autor |
| Mapy / terény (PNG soubory) | autor: AI generated |
| UI elementy (ikony kouzel, tlačítka) | autor: / |
| Efekty kouzel (ohnivá koule, blesk) | autor: Autor |

### Free assety z Unity Asset Store
| Název assetu | Autor / Odkaz | Licence |
|---|---|---|
| Buttons Set | https://assetstore.unity.com/packages/2d/gui/buttons-set-211824 | Unity Asset Store |
| Alien Tile set | https://assetstore.unity.com/packages/2d/textures-materials/nature/alien-tile-set-116827 | Unity Asset Store |

---

## 📚 Použité zdroje a dokumentace

### Unity dokumentace
- [Rigidbody2D](https://docs.unity3d.com/Manual/class-Rigidbody2D.html)
- [Texture2D – SetPixels](https://docs.unity3d.com/ScriptReference/Texture2D.SetPixels.html)
- [PolygonCollider2D](https://docs.unity3d.com/Manual/class-PolygonCollider2D.html)
- [Physics2D](https://docs.unity3d.com/Manual/Physics2DReference.html)
- [JsonUtility](https://docs.unity3d.com/ScriptReference/JsonUtility.html)
- [LineRenderer (lano / grappling hook)](https://docs.unity3d.com/Manual/class-LineRenderer.html)
- [SceneManagement](https://docs.unity3d.com/Manual/MultiSceneEditing.html)

