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
| **Herní mód** | 2 hráči na jednom PC (hotseat) + PVE |

---

## 🎮 Popis hry

Tahová 2D fantasy hra pro dva hráče na jednom PC. Každý hráč ovládá tým myší-válečníků na destruktibilním terénu. Střídavě využívají magické schopnosti a zbraně, aby eliminovali soupeřův tým. Terén je načten z vlastního PNG obrázku a lze ho v průběhu hryničit kouzly a výbuchy.

---

## ✅ Funkcionality

### Herní systém
- Tahový systém – hráči se střídají, každý tah má časový limit (30 sekund)
- Systém HP – každá myš má životy, při 0 HP umírá
- Pád myší po zničení terénu pod nimi
- Smrt pádem do propasti (mimo hrací plochu)

### Terén
- Destruktibilní terén načtený z vlastního PNG obrázku
- Kouzla a výbuchy vytvoří kruhový výřez v terénu (pixel destruction)
- 3 různé mapy s fantasy tématikou – jeskyně, les, hrad

### ⚔️ Zbraně & Kouzla

| Název | Typ | Popis |
|---|---|---|
| 🔥 **Ohnivá koule** | Kouzlo | Vystřelí hořící projektil s obloukem, výbuch při dopadu |
| ⚡ **Blesk** | Kouzlo | Přímý paprsek zasahující první překážku nebo myš |
| 🏹 **Luk a šíp** | Zbraň | Šíp s fyzikou oblouku, menší ale přesný damage |
| 💣 **Magická bomba** | Kouzlo | Hozená s časovačem, velký poloměr výbuchu |
| 🪝 **Lano (grappling hook)** | Pohyb | Vystřelí lano pro přesun na vzdálené místo |
| 👊 **Úder (melee)** | Zbraň | Silný úder na blízko odhodí soupeře |
| 🌀 **Teleportace** | Kouzlo | Okamžitý přesun myši na kliknuté místo |

### Fyzika
- Fyzika projektilů – gravitace, oblouk letu (Unity Rigidbody2D)
- Poloměr výbuchu – poškození myší dle vzdálenosti
- Knockback – odhození myší silou výbuchu nebo melee útoku
- Pohyb myší – chůze, skok, lano

### Ukládání dat
- Ukládání výsledků zápasů a high score (JSON)
- Načítání map z PNG souborů

### UI
- Hlavní menu – nová hra, výběr mapy, quit
- HUD – životy myší, aktuální zbraň/kouzlo, časovač tahu, kolo
- End screen – zobrazení výherce

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
| Grafika myší a animace | autor:  |
| Mapy / terény (PNG soubory) | autor: ] |
| UI elementy (ikony kouzel, tlačítka) | autor:  |
| Efekty kouzel (ohnivá koule, blesk) | autor:  |

### Free assety z Unity Asset Store
| Název assetu | Autor / Odkaz | Licence |
|---|---|---|
| *(bude doplněno)* | *(bude doplněno)* | Unity Asset Store |

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

