# Настройка Radzen для графика статистики

## Текущая конфигурация

### 1. CDN подключение (wwwroot/index.html)
```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@radzenhq/radzen@5.32.5/css/material-base.css">
<script src="https://cdn.jsdelivr.net/npm/@radzenhq/radzen@5.32.5/dist/Radzen.Blazor.js"></script>
```

### 2. App.razor
```razor
<RadzenStyleSheet />
<RadzenScripts />
```

### 3. Program.cs - сервисы
```csharp
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
```

### 4. _Imports.razor
```razor
@using Radzen;
@using Radzen.Blazor;
```

## Если график не работает

1. Проверьте интернет соединение
2. Очистите кэш браузера
3. Перезапустите сервер разработки
