# Chestniy_znack

Шаблонный Avalonia-проект, который можно установить в `dotnet new`.

Установка локального шаблона:

```powershell
dotnet new install D:\Project\QualityDesigner\Chestniy_znack
```

Создание нового проекта:

```powershell
dotnet new qualitydesigner -n MyDesktopApp
```

После создания шаблон сохраняет:

- стили окна и базовых контролов;
- верхнюю панель с управлением окном;
- боковую навигацию;
- папки `Views`, `Views/Components`, `Views/Pages`;
- папки `ViewModels` и `ViewModels/Base`;
- примеры привязок `View <-> ViewModel`.

Дальше обычно меняют:

- тексты-заглушки;
- демонстрационные страницы;
- ViewModel под свою предметную область;
- сервисы, модели и реальные данные.
