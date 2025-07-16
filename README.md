# 📅 Calendário Pessoal - .NET MAUI

Um aplicativo de calendário multiplataforma desenvolvido com .NET MAUI e SQLite, com design moderno e funcional.

## 🚀 Funcionalidades

- ✅ **Calendário Visual Interativo**: Navegação por meses com visual inspirado no design fornecido
- ✅ **Persistência de Dados**: Todos os eventos são salvos localmente usando SQLite
- ✅ **Gestão de Eventos**: Criar, editar e deletar eventos
- ✅ **Categorização**: Organize eventos por categorias (Trabalho, Pessoal, Saúde, etc.)
- ✅ **Cores Personalizáveis**: Cada evento pode ter uma cor diferente
- ✅ **Multiplataforma**: Funciona em Android, iOS, Windows e macOS
- ✅ **Interface Responsiva**: Design otimizado para smartphones

## 🛠️ Tecnologias Utilizadas

- **.NET MAUI 8.0**: Framework multiplataforma
- **SQLite**: Banco de dados local
- **MVVM Pattern**: Arquitetura Model-View-ViewModel
- **Data Binding**: Ligação de dados reativa
- **Shell Navigation**: Sistema de navegação do MAUI

## 📁 Estrutura do Projeto

```
CalendarioApp/
├── Models/
│   └── CalendarEvent.cs          # Modelo de dados dos eventos
├── Services/
│   └── DatabaseService.cs       # Serviço de acesso aos dados
├── ViewModels/
│   └── CalendarViewModel.cs      # Lógica de apresentação
├── Views/
│   ├── CalendarPage.xaml         # Página principal do calendário
│   └── EventDetailPage.xaml      # Página de detalhes/edição
├── Converters/
│   └── ValueConverters.cs        # Conversores de valores
├── App.xaml                      # Configurações globais
├── AppShell.xaml                 # Navegação principal
└── MauiProgram.cs                # Configuração da aplicação
```

## 🏗️ Pré-requisitos

- **Visual Studio 2022**
- **.NET 8.0 SDK**
- **Workload do .NET MAUI** instalado

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 👨‍💻 Autor

Desenvolvido por [Alan Oliveira](https://github.com/Alan-oliveir).
