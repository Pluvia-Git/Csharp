# 🌧️ Pluvia

*Pluvia* é uma aplicação ASP.NET Core (com Razor Pages e API REST) que permite registrar usuários com sua localização geográfica e consultar, em tempo real, o clima e o risco de alagamento da cidade correspondente.

---

## 🚀 Funcionalidades

- ✅ Registro de usuários com latitude, longitude e cidade
- ✅ Consulta do clima atual via API externa (Open-Meteo)
- ✅ Verificação do risco de alagamento por cidade (dados simulados)
- ✅ Interface Swagger para testar a API
- ✅ Página Razor para visualização simples do resultado

---

## 🧱 Tecnologias

- .NET 8
- Entity Framework Core 7 (por compatibilidade com Oracle)
- Oracle Database
- Swagger (Swashbuckle)
- Razor Pages
- Open-Meteo API

---

## 🗃️ Banco de Dados (Oracle)

*Tabelas principais:*

- Usuarios: id, nome, latitude, longitude, cidadeId
- Cidades: id, nome
- RiscoAlagamento: id, data, nivelRisco, cidadeId

*Relacionamentos:*

- Uma cidade possui *muitos usuários*
- Uma cidade possui *muitos registros de risco de alagamento*

---

## ⚙️ Como executar o projeto

### 1. Clone o repositório

bash
git clone https://github.com/seu-usuario/pluvia.git
cd pluvia


### 2. Ajuste a string de conexão no appsettings.json

json
"ConnectionStrings": {
  "DefaultConnection": "User Id=RMXXXXXX;Password=senha;Data Source=localhost:1521/ORCL;"
}


### 3. Crie o banco com EF Core

bash
dotnet ef migrations add InitialCreate
dotnet ef database update


### 4. Rode o projeto

bash
dotnet run


---

## 🔍 Acessos rápidos

- Swagger: [http://localhost:5000/swagger](http://localhost:5000/swagger)
- Página Razor: [http://localhost:5000](http://localhost:5000)

---

## 📬 Endpoints da API

| Método | Rota                          | Descrição                                 |
|--------|-------------------------------|-------------------------------------------|
| GET    | /api/usuarios               | Lista todos os usuários                   |
| GET    | /api/usuarios/{id}/clima    | Mostra o clima do usuário via Open-Meteo |
| GET    | /api/usuarios/{id}/risco-alagamento | Mostra o risco da cidade do usuário |
| POST   | /api/usuarios               | Cria um novo usuário                      |

---

## 📥 Exemplo de body para POST /api/usuarios

```sh
{
  "nome": "Gabriel Gomes",
  "email": "gabrielgomes@gmail.com",
  "cpf": "24431782012",
  "senha": "123456",
  "endereco": {
    "cidade": "Rio de Janeiro",
    "bairro": "Copacabana",
    "logradouro": "Avenida Atlântica",
    "uf": "RJ",
    "cep": "22040002",
    "latitude": -22.971964,
    "longitude": -43.182545
  }
}
```

---

## 👨‍💻 Desenvolvedores
Thomas Rodrigues Ribeiro Silva RM558042 2TDSPK
João Victor Rocha Cândido RM 554727 2TDSPK
João Vitor Broggine Lopes RM 557129 2TDSPF
