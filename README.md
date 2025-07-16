# 📅 Calendário Pessoal - .NET MAUI

<div align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-purple?style=for-the-badge&logo=dotnet" alt=".NET 8.0" />
  <img src="https://img.shields.io/badge/MAUI-Cross--Platform-blue?style=for-the-badge&logo=xamarin" alt="MAUI" />
  <img src="https://img.shields.io/badge/SQLite-Database-green?style=for-the-badge&logo=sqlite" alt="SQLite" />
  <img src="https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge" alt="MIT License" />
</div>

<br />

<div align="center">
  <h3>Um aplicativo de calendário multiplataforma com design moderno e funcional</h3>
  <p>Desenvolvido com .NET MAUI e SQLite para Android, iOS, Windows e macOS</p>
</div>

<div align="center">
  <a href="#-sobre-o-projeto">Sobre</a> •
  <a href="#-funcionalidades">Funcionalidades</a> •
  <a href="#-instalação">Instalação</a> •
  <a href="#-tecnologias">Tecnologias</a> •
  <a href="#-contribuindo">Contribuindo</a> •
  <a href="#-licença">Licença</a>
</div>

<br />

## 🎯 Sobre o Projeto

Este é um aplicativo de calendário pessoal multiplataforma que permite organizar eventos, tarefas e compromissos de forma intuitiva. Com uma interface moderna e responsiva, oferece uma experiência consistente em Android, iOS, Windows e macOS.

### 🌟 Principais Características

- 📱 **Interface Intuitiva**: Design moderno inspirado em aplicativos nativos
- 🎨 **Personalização**: Cores e categorias personalizáveis
- 💾 **Offline First**: Funciona completamente offline com SQLite
- 🌐 **Multiplataforma**: Um código, todas as plataformas

## 📸 Screenshots

> 🚧 **Em desenvolvimento** - Screenshots serão adicionados em breve

| Desktop | Mobile | Tablet |
|---------|--------|--------|
| *Em breve* | *Em breve* | *Em breve* |

## ✨ Funcionalidades

### 📱 Interface do Usuário
- ✅ **Calendário Visual Interativo** - Navegação fluida por meses
- ✅ **Interface Responsiva** - Layout otimizado para todos os dispositivos
- ✅ **Indicadores Visuais** - Pontos coloridos para dias com eventos

### 🗂️ Gestão de Eventos
- ✅ **Criar Eventos** - Título, descrição, categoria e cor personalizável
- ✅ **Deletar Eventos** - Remoção rápida de eventos
- ✅ **Visualização por Dia** - Lista detalhada dos eventos do dia

### 🎨 Personalização
- ✅ **Cores Personalizáveis** - Organize eventos por cores
- ✅ **Sistema de Categorias** - Trabalho, Pessoal, Saúde, etc.

### 💾 Armazenamento
- ✅ **SQLite Local** - Dados salvos localmente
- ✅ **Funcionamento Offline** - Sem necessidade de internet
- ✅ **Performance Otimizada** - Acesso rápido aos dados

### 🌐 Plataformas Suportadas

| Plataforma | Versão Mínima | Status |
|-----------|---------------|---------|
| **Android** | API 21 (5.0+) | ✅ Suportado |
| **iOS** | 11.0+ | ✅ Suportado |
| **Windows** | 10 Build 17763+ | ✅ Suportado |
| **macOS** | 10.15+ | ✅ Suportado |

## 🏗️ Arquitetura do Projeto

O projeto segue as melhores práticas de desenvolvimento .NET MAUI com padrão **MVVM**:

```
Calendar/
├── � Models/
│   └── CalendarEvent.cs              # Modelo de dados dos eventos
├── � Services/
│   └── DatabaseService.cs            # Serviço de acesso aos dados SQLite
├── � ViewModels/
│   └── CalendarViewModel.cs          # Lógica de apresentação do calendário
├── � Views/
│   ├── CalendarPage.xaml             # Página principal do calendário
│   └── EventDetailPage.xaml          # Página de criação/edição de eventos
├── � Converters/
│   └── ValueConverters.cs            # Conversores para data binding
├── � Resources/
│   ├── � AppIcon/                   # Ícones da aplicação
│   ├── � Splash/                    # Tela de splash
│   └── � Fonts/                     # Fontes personalizadas
├── App.xaml                          # Configurações globais
├── AppShell.xaml                     # Navegação principal
├── MauiProgram.cs                    # Configuração da aplicação
└── README.md                         # Documentação
```

### 🔧 Padrões Implementados

- **MVVM (Model-View-ViewModel)** - Separação clara de responsabilidades
- **Dependency Injection** - Injeção de dependências nativa do .NET MAUI
- **Data Binding** - Ligação reativa entre View e ViewModel
- **Command Pattern** - Comandos para ações da interface
- **Repository Pattern** - Acesso a dados através do DatabaseService

### 📊 Modelo de Dados

```csharp
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
```


## 🚀 Instalação e Configuração

### 📋 Pré-requisitos

Certifique-se de ter os seguintes componentes instalados:

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

**Para macOS:**
```bash
# Instalar dependências do iOS
sudo xcode-select --install

# Instalar workload do MAUI
dotnet workload install maui
```

## 🎨 Design e Interface

### 🎨 Paleta de Cores

<div align="center">

| Cor | Hex | Uso |
|-----|-----|-----|
| ![#6366f1](https://via.placeholder.com/15/6366f1/000000?text=+) | `#6366f1` | Cor Primária (Índigo) |
| ![#8b5cf6](https://via.placeholder.com/15/8b5cf6/000000?text=+) | `#8b5cf6` | Cor Secundária (Violeta) |
| ![#10b981](https://via.placeholder.com/15/10b981/000000?text=+) | `#10b981` | Cor de Destaque (Esmeralda) |
| ![#ffffff](https://via.placeholder.com/15/ffffff/000000?text=+) | `#ffffff` | Superfície (Branco) |
| ![#f8fafc](https://via.placeholder.com/15/f8fafc/000000?text=+) | `#f8fafc` | Fundo (Cinza claro) |

</div>

### 🎯 Componentes Visuais

- **Botões circulares** para navegação intuitiva
- **Cards com sombras** para destacar eventos
- **Indicadores coloridos** para dias com eventos
- **Animações suaves** de transição entre páginas
- **Design responsivo** que se adapta a diferentes tamanhos de tela

## 🤝 Contribuindo

Contribuições são sempre bem-vindas! Sinta-se à vontade para contribuir com o projeto.

### 🛠️ Como Contribuir

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

### 📝 Diretrizes de Contribuição

- ✅ Siga as convenções de código existentes
- ✅ Adicione testes para novas funcionalidades
- ✅ Atualize a documentação quando necessário
- ✅ Mantenha commits pequenos e focados
- ✅ Use mensagens de commit descritivas

### 🐛 Reportando Bugs

Encontrou um bug? Ajude-nos a corrigi-lo:

1. Verifique se o bug já não foi reportado nas [Issues](https://github.com/Alan-oliveir/Calendar/issues)
2. Crie uma nova issue com detalhes sobre o problema
3. Inclua screenshots se aplicável
4. Descreva os passos para reproduzir o bug

### 💡 Sugestões de Funcionalidades

Tem uma ideia para melhorar o app? Compartilhe conosco:

1. Abra uma issue com a tag `enhancement`
2. Descreva detalhadamente sua sugestão
3. Explique como isso beneficiaria os usuários

## ❓ FAQ

<details>
<summary><strong>📱 Em quais dispositivos posso usar o app?</strong></summary>

O app funciona em:
- **Android**: Smartphones e tablets com Android 5.0+
- **iOS**: iPhone e iPad com iOS 11.0+
- **Windows**: PCs com Windows 10 Build 17763+
- **macOS**: Macs com macOS 10.15+

</details>

<details>
<summary><strong>💾 Meus dados ficam seguros?</strong></summary>

Sim! Todos os dados são armazenados localmente no seu dispositivo usando SQLite. Não enviamos nenhuma informação para servidores externos.

</details>

<details>
<summary><strong>🌐 Preciso de internet para usar?</strong></summary>

Não! O app funciona completamente offline. Todos os recursos estão disponíveis sem conexão com a internet.

</details>

## � Status do Projeto

![GitHub last commit](https://img.shields.io/github/last-commit/Alan-oliveir/Calendar/)
![GitHub issues](https://img.shields.io/github/issues/Alan-oliveir/Calendar/)
![GitHub pull requests](https://img.shields.io/github/issues-pr/Alan-oliveir/Calendar/)

**Status atual:** 🚧 Em desenvolvimento ativo

## 📞 Suporte e Contato

### 🆘 Precisa de Ajuda?

1. **Consulte a documentação** - Verifique este README
2. **Procure nas Issues** - [Issues existentes](https://github.com/Alan-oliveir/Calendar/issues)
3. **Crie uma nova Issue** - Relate problemas ou sugestões
4. **Entre em contato** - Através do GitHub

### 📧 Contato

- **GitHub**: [@Alan-oliveir](https://github.com/Alan-oliveir)
- **Email**: [Disponível no perfil GitHub](https://github.com/Alan-oliveir)

### 🤝 Comunidade

Junte-se à nossa comunidade:
- ⭐ **Dê uma estrela** no projeto
- 🍴 **Faça um fork** para contribuir
- 🐛 **Reporte bugs** via Issues
- 💡 **Sugira melhorias** via Issues

## 📄 Licença

Este projeto está licenciado sob a **Licença MIT** - veja o arquivo [LICENSE](LICENSE) para detalhes.

## 📊 Estatísticas

<div align="center">

![GitHub repo size](https://img.shields.io/github/repo-size/Alan-oliveir/Calendar)
![GitHub code size](https://img.shields.io/github/languages/code-size/Alan-oliveir/Calendar)
![Lines of code](https://img.shields.io/tokei/lines/github/Alan-oliveir/Calendar)
![GitHub top language](https://img.shields.io/github/languages/top/Alan-oliveir/Calendar)

</div>

---

<div align="center">
  <h3>✨ Obrigado por usar o Calendário Pessoal! ✨</h3>
  <p>Desenvolvido por <a href="https://github.com/Alan-oliveir">Alan Oliveira</a></p>
  <p>
    <a href="https://github.com/Alan-oliveir/Calendar/stargazers">⭐ Dê uma estrela</a> •
    <a href="https://github.com/Alan-oliveir/Calendar/fork">🍴 Faça um fork</a> •
    <a href="https://github.com/Alan-oliveir/Calendar/issues">🐛 Reporte bugs</a>
  </p>
  <p><strong>Se este projeto te ajudou, considere dar uma estrela! 🌟</strong></p>
</div>
