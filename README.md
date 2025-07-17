# 📅 Agenda Pessoal - .NET MAUI

<div align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-purple?style=for-the-badge&logo=dotnet" alt=".NET 8.0" />
  <img src="https://img.shields.io/badge/MAUI-Cross--Platform-blue?style=for-the-badge&logo=xamarin" alt="MAUI" />
  <img src="https://img.shields.io/badge/SQLite-Database-green?style=for-the-badge&logo=sqlite" alt="SQLite" />
  <img src="https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge" alt="MIT License" />
</div>

<div align="center">
  <h3>Um aplicativo de calendário e tarefas multiplataforma com design moderno e funcional</h3>
  <p>Desenvolvido com .NET MAUI e SQLite para Android e Windows</p>
</div>

<div align="center">
  <a href="#-sobre-o-projeto">Sobre</a> •
  <a href="#-screenshots">Screenshots</a> •
  <a href="#-funcionalidades">Funcionalidades</a> •
  <a href="#-instalação">Instalação</a> •
  <a href="#arquitetura-do-projeto">Arquitetura</a> •  
  <a href="#-tecnologias">Tecnologias</a> •
  <a href="#-contribuindo">Contribuindo</a> •
  <a href="#-licença">Licença</a>
</div>

## 🎯 Sobre o Projeto

![GitHub repo size](https://img.shields.io/github/repo-size/Alan-oliveir/Calendar)
![GitHub code size](https://img.shields.io/github/languages/code-size/Alan-oliveir/Calendar)
![GitHub top language](https://img.shields.io/github/languages/top/Alan-oliveir/Calendar)

Este é um aplicativo de calendário pessoal multiplataforma que permite organizar eventos, tarefas e compromissos de forma intuitiva. Com uma interface moderna e responsiva, oferece uma experiência consistente em Android e Windows. O projeto foi desenvolvido como trabalho final da disciplina Software para Smartphone, do curso de Engenharia Eletrônica e de Computação da UFRJ.

### 🌟 Principais Características

- 📱 **Interface Intuitiva**: Design moderno inspirado em aplicativos nativos
- 💾 **Offline**: Funciona completamente offline com SQLite
- 🌐 **Multiplataforma**: Um código, todas as plataformas (com foco em Mobile)

## 📸 Screenshots

<div align="center">
  <img src="https://github.com/Alan-oliveir/Calendar/blob/master/Screenshots/screenshot_calendario.jpg" alt="Calendário" width="320"/>
  &nbsp;
  <img src="https://github.com/Alan-oliveir/Calendar/blob/master/Screenshots/screenshot_anota%C3%A7%C3%B5es.jpg" alt="Anotações" width="320"/>
  &nbsp;
  <img src="https://github.com/Alan-oliveir/Calendar/blob/master/Screenshots/screenshot_tarefa.jpg" alt="Tarefas" width="320"/>
</div>

## ✨ Funcionalidades

### 📅 **Módulo Calendário**
- ✅ **Visualização Mensal Interativa** - Navegação fluida entre meses
- ✅ **Gestão de Eventos** - Criação e exclusão de eventos
- ✅ **Indicadores Visuais** - Pontinhos coloridos para dias com eventos
- ✅ **Categorização** - Organização por categorias (Trabalho, Pessoal, Saúde, etc.)

### ⏰ **Módulo Horários (Compromissos)**
- ✅ **Agendamento de Compromissos** - Data, horário de início e fim
- ✅ **Gestão de Reuniões** - Criação, edição e exclusão de reuniões
- ✅ **Visualização Cronológica** - Lista ordenada por data e horário

### ✅ **Módulo Tarefas (Lista de Tarefas)**
- ✅ **Criação de Tarefas** - Título, descrição, prioridade e data de vencimento
- ✅ **Controle de Status** - Marcar como concluída com checkbox
- ✅ **Filtros Inteligentes** - Visualizar todas, pendentes ou concluídas
- ✅ **Edição e Exclusão** - Botões de ação otimizados para mobile

### 📝 **Módulo Anotações (Diário)**
- ✅ **Criação de Notas** - Título e conteúdo
- ✅ **Ordenação Cronológica** - Notas mais recentes primeiro
- ✅ **Interface Limpa** - Foco na escrita e leitura

### 💾 **Armazenamento**
- ✅ **SQLite Local** - Banco de dados robusto e eficiente
- ✅ **Funcionamento Offline** - Sem necessidade de conexão

### 📱 Interface do Usuário
- ✅ **Interface Responsiva** - Layout otimizado para dispositivos móveis
- ✅ **Indicadores Visuais** - Pontos coloridos para dias com eventos

### 🌐 Plataformas

| Plataforma | Versão Mínima | Status |
|-----------|---------------|---------|
| **Android** | API 21 (5.0+) | ✅ Suportado |
| **Windows** | 10 Build 17763+ | ✅ Suportado |

## 🏗️ Arquitetura do Projeto

O projeto segue as melhores práticas de desenvolvimento .NET MAUI com padrão **MVVM**:

```
Calendar/
├── 📁 Models/
│   ├── CalendarEvent.cs              # Modelo de dados dos eventos
│   ├── Appointment.cs                # Modelo de dados dos compromissos
│   ├── TaskItem.cs                   # Modelo de dados das tarefas
│   └── DiaryNote.cs                  # Modelo de dados das anotações
├── 📁 Services/
│   └── DatabaseService.cs            # Serviço unificado de acesso aos dados
├── 📁 ViewModels/
│   ├── CalendarViewModel.cs          # Lógica do calendário
│   ├── ScheduleViewModel.cs          # Lógica dos horários
│   ├── TasksViewModel.cs             # Lógica das tarefas
│   └── DiaryViewModel.cs             # Lógica das anotações
├── 📁 Views/
│   ├── CalendarPage.xaml             # Página do calendário
│   ├── SchedulePage.xaml             # Página dos horários
│   ├── TasksPage.xaml                # Página das tarefas
│   ├── DiaryPage.xaml                # Página das anotações
│   └── EventDetailPage.xaml          # Página de detalhes do evento
├── 📁 Converters/
│   └── ValueConverters.cs            # Conversores para data binding
├── 📁 Resources/
│   ├── 📁 AppIcon/                   # Ícones da aplicação
│   ├── 📁 Splash/                    # Tela de splash
│   ├── 📁 Images/                    # Imagens e recursos visuais
│   └── 📁 Fonts/                     # Fontes personalizadas
├── App.xaml                          # Configurações globais da aplicação
├── AppShell.xaml                     # Navegação principal (TabBar)
├── MauiProgram.cs                    # Configuração e DI
└── README.md                         # Documentação do projeto
```

### 🔧 Padrões Implementados

- **MVVM (Model-View-ViewModel)** - Separação clara de responsabilidades
- **Dependency Injection** - Injeção de dependências nativa do .NET MAUI
- **Data Binding** - Ligação reativa entre View e ViewModel
- **Command Pattern** - Comandos para ações da interface
- **Repository Pattern** - Acesso a dados através do DatabaseService
- **Single Responsibility** - Cada classe tem uma responsabilidade específica

### 📊 Modelo de Dados

```csharp
// Eventos do Calendário
public class CalendarEvent
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Color { get; set; }
    public bool IsAllDay { get; set; }
}

// Compromissos/Reuniões
public class Appointment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
}

// Tarefas
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public string Priority { get; set; }
    public bool IsCompleted { get; set; }
}

// Anotações
public class DiaryNote
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

## 🚀 Instalação

### 📋 Pré-requisitos

- **Visual Studio 2022** (versão 17.8 ou superior)
- **.NET 8.0 SDK**
- **Workload do .NET MAUI** instalado no Visual Studio

### ⚡ Início Rápido

1. **Clone o repositório**
   ```bash
   git clone https://github.com/Alan-oliveir/Calendar.git
   cd Calendar
   ```

2. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

3. **Execute o projeto**
   
   **Para Windows:**
   ```bash
   dotnet run --framework net8.0-windows10.0.19041.0
   ```
   
   **Para Android (com dispositivo conectado):**
   ```bash
   dotnet run --framework net8.0-android
   ```

### 📱 Configuração para Android

1. **Habilite o Modo Desenvolvedor** no dispositivo Android
2. **Ative a Depuração USB**
3. **Conecte o dispositivo** via USB
4. **Execute o projeto** selecionando seu dispositivo no Visual Studio

### 🛠️ Configuração do Ambiente de Desenvolvimento

**Para Windows:**
```bash
# Verificar se o .NET 8.0 está instalado
dotnet --version

# Instalar workload do MAUI
dotnet workload install maui
```

## 💻 Tecnologias

### 🔧 **Principais Tecnologias**

- **.NET 8.0** - Framework principal
- **.NET MAUI** - Framework multiplataforma
- **SQLite** - Banco de dados local
- **XAML** - Linguagem de marcação para UI
- **C#** - Linguagem de programação principal

### 🛠️ **Ferramentas de Desenvolvimento**

- **Visual Studio 2022** - IDE principal
- **Git** - Controle de versão

## 🤝 Contribuindo

1. **Faça um Fork** do projeto
2. **Crie uma branch** para sua feature
   ```bash
   git checkout -b feature/MinhaNovaFeature
   ```
3. **Commit** suas mudanças
   ```bash
   git commit -m 'feat: Adiciona nova funcionalidade'
   ```
4. **Push** para a branch
   ```bash
   git push origin feature/MinhaNovaFeature
   ```
5. **Abra um Pull Request**

## 🤖 Uso de IA

Durante o desenvolvimento do projeto foi utilizado o **GitHub Copilot** como ferramenta para auxiliar na identificação e correção de erros, bem como para sugestões de melhorias na estrutura do código. Também foi utilizado o **DALL-E (OpenAI)** para criar ícones, splash screen e sugerir design para a interface da aplicação. 
       
## 📄 Licença
Este projeto está licenciado sob a Licença MIT. Veja o arquivo [LICENSE](https://github.com/Alan-oliveir/Calendar/blob/master/LICENSE.txt).

---

### Agradecimentos
Ao professor **Manuel Villas Boas Junior** pelas aulas ministradas na disciplina *Software para smartphone*.

### Aluno
Desenvolvido por **Alan de O. Gonçalves** como trabalho final da disciplina *Software para Smartphone*.

[![Github](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/Alan-oliveir)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/alan-ogoncalves)
