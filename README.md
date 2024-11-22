# 🎯 Sniper Beginner

---

# 👨‍🏫 프로젝트 소개

- **스나이퍼 엘리트** 시리즈에 영감받아 제작한 FPS 잠입 액션 게임의 프로토타입입니다.
- 플레이어는 적의 감시를 피해 목표를 제거하며, 긴장감 넘치는 잠입과 정밀한 저격 메커니즘으로 몰입감 있는 게임플레이를 제공합니다.

---

# ⏲️ 개발 기간

- **2024.11.15(금) ~ 2024.11.22(금)**

---

# 🧑‍🤝‍🧑 개발자 소개

- **권예림 : Enemy, AI**

- **김정환 : Player, Aim, Shoot**

- **김지훈 : QuickSlot, MiniMap, ParticleSystem, Sound**

- **신윤영 : Save/Load, Cinemachine**

---

# 🛠️ 기술 스택 (Tech Stack)

### 언어
- **C#**

### 프레임워크 / 엔진
- **Unity 2022.3.17f1 (LTS)**

### 라이브러리 및 SDK
- **Cinemachine**: 카메라 제어 및 연출.

### 툴
- **Unity Editor**
- **Visual Studio 2022**
- **Git (버전 관리)**

---

# 🌍 지원 플랫폼

- **Windows**

---

# 🌟 주요 기능

### 🔹 잠입 및 전술적 플레이
- **실시간 적 감지 AI**  
  적의 시야를 기반으로 설계된 감지 시스템.

### 🔹 시네머신 시스템
- **카메라 연출**  
  저격 성공 시의 타격감을 극대화하는 슬로우 모션 연출.
  - 저격 시 카메라가 목표를 따라가는 영화 같은 연출 제공.

### 🔹 긴박한 분위기의 BGM
- 긴장감을 고조시키는 저음 기반의 배경음악.

### 🔹 퀵슬롯 시스템
- **무기 및 장비 변경**  
  빠르고 직관적인 UI로 플레이어가 전투 상황에서 무기나 아이템을 빠르게 전환 가능.
  - 어썰트 라이플, 스나이퍼 라이플 무기 간 전환.
  
### 🔹 저장 시스템
- **플레이 데이터 저장 및 불러오기**  
  게임 진행 상황을 언제든 저장하고, 이후 저장된 상태에서 이어서 플레이 가능.
  - 자동 저장 기능 지원.
  - 플레이어 위치, 장비 상태, 적 위치를 포함한 상태 저장.

### 🔹 일반 사격 / 저격 분리 및 부위 타격
- **일반 사격과 저격의 전환**  
  상황에 따라 일반 사격(근거리)과 저격(원거리)을 자유롭게 전환.
- **부위 타격 효과**  
  적의 특정 부위별 데미지 배율 차별화.
  - 헤드샷: 10배 배율.
  - 가슴 타격: 5배 배율.

### 🔹 미니맵
- **실시간 정보 제공**  
  적의 위치, 플레이어의 목표 지점을 표시하는 직관적인 미니맵.
  - 플레이어의 현재 위치와 방향 표시.
  - 적의 현재 위치 표시.

---

# 🚀 설치 및 실행 방법

1. **Unity 설치**
   - Unity Hub를 통해 **Unity 2022.3.17f1 (LTS)** 버전을 설치합니다.

2. **프로젝트 열기**
   - Unity Editor에서 `Sniper Beginner` 프로젝트 폴더를 열어주세요.

3. **플랫폼 설정**
   - `File > Build Settings`에서 플랫폼을 **Windows**로 설정합니다.

4. **빌드 및 실행**
   - `Build and Run` 버튼을 눌러 실행 파일을 생성하고 게임을 실행합니다.

---

# 🎥 데모 및 스크린샷

### 데모 영상

[YouTube 데모 영상](https://youtube.com/yourdemo)

### 스크린샷

### 타이틀 화면
<img src="https://github.com/user-attachments/assets/05c43a77-fa00-4b1a-8de3-90afbb51d606" width="600"/>

> **게임 시작 전 타이틀 화면** - 깔끔한 UI와 메뉴.

---

### 미니맵 UI
<img src="https://github.com/user-attachments/assets/dfe4c869-fc38-442d-a8f0-1af00a415100" width="600"/>

> **실시간 미니맵** - 적의 위치와 목표 지점을 시각적으로 확인 가능.

---

### 일반 사격
<img src="https://github.com/user-attachments/assets/52094a52-e9e9-4d66-8d38-0fe317e0d314" width="600"/>

> **일반 사격** - 상황에 따라 무기를 전환해 전술적으로 플레이 가능.

---

### 저격 모드
<img src="https://github.com/user-attachments/assets/fe1e56a2-d922-425d-be9b-4a4c42e0686a" width="600"/>

> **시네머신** - 몰입감을 더하는 세부 연출.

---

# 📚 참고 자료

- [Sniper Elite 시리즈 공식 웹사이트](https://sniperelite.com/ko-kr)
- [Unity Documentation](https://docs.unity3d.com/Manual/index.html)
- [Cinemachine Documentation](https://docs.unity3d.com/Packages/com.unity.cinemachine@2.6/manual/index.html)

---
