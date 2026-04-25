# 🚀 Ordem de Serviço App

Sistema de gerenciamento de chamados técnicos, desenvolvido como teste técnico.

---

## 🧰 Tecnologias Utilizadas

### 🔙 Backend

* .NET 8 (ASP.NET Core Web API)
* Entity Framework Core
* SQLite (persistência local)
* JWT (autenticação)
* Worker Service (processamento assíncrono)

### 🎨 Frontend

* ReactJS
* Vite
* JavaScript (Fetch API)

### 🧱 Arquitetura

* Separação em camadas:

  * Domain
  * Infrastructure
  * API
  * Worker
* Repository Pattern
* Princípios SOLID

---

## 📌 Funcionalidades

* Cadastro de chamados técnicos
* Listagem de chamados
* Filtro por status, prioridade e cliente
* Atualização de status
* Autenticação via JWT
* Processamento assíncrono com Worker

---

## ⚠️ Observação Importante

O teste solicitava o uso de:

* PostgreSQL
* MongoDB
* RabbitMQ

Porém, devido a **restrições de ambiente (sem permissões administrativas para instalação de serviços locais)**, foi adotada a seguinte abordagem:

* Uso de **SQLite** para persistência local
* Uso de **dados mockados/simulados**
* Mensageria simulada em memória

👉 A arquitetura foi estruturada de forma que essas tecnologias possam ser facilmente substituídas em um ambiente com permissões adequadas.

---

## ▶️ Como Executar o Projeto

### 🔧 Backend (API)

```bash
dotnet run --project OrdemServico.API
```

Acesse o Swagger:

```
http://localhost:5008/swagger
```

---

### ⚙️ Worker

```bash
dotnet run --project OrdemServico.Worker
```

---

### 💻 Frontend

```bash
cd ordem-servico-ui
npm install
npm run dev
```

---

## 🔐 Autenticação

Endpoint de login:

```
POST /api/Auth/login
```

Exemplo:

```json
{
  "username": "paulo",
  "password": "123"
}
```

Utilize o token retornado no Swagger ou frontend com:

```
Bearer {token}
```

---

## 🧠 Considerações Técnicas

* O projeto segue boas práticas de organização e separação de responsabilidades
* Estrutura preparada para evolução com bancos reais e mensageria externa
* Código organizado visando legibilidade e manutenção

---

## 📎 Repositório

Este repositório contém todo o código necessário para execução do projeto.
