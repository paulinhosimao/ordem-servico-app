import { useState, useEffect } from 'react';
import API from './api';
import './App.css';

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(!!localStorage.getItem('token'));
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [chamados, setChamados] = useState([]);
  const [filtro, setFiltro] = useState('');
  const [cliente, setCliente] = useState('');
  const [descricao, setDescricao] = useState('');
  const [prioridade, setPrioridade] = useState(1);
  useEffect(() => {
    if (isLoggedIn) {
      fetchChamados();
    }
  }, [isLoggedIn, filtro]);
  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const response = await API.post('/auth/login', { username, password });
      localStorage.setItem('token', response.data.token);
      setIsLoggedIn(true);
      setUsername('');
      setPassword('');
    } catch (error) {
      alert('Erro ao fazer login');
    }
  };
  const handleLogout = () => {
    localStorage.removeItem('token');
    setIsLoggedIn(false);
    setChamados([]);
  };
  const fetchChamados = async () => {
    try {
      const url = filtro ? `/chamados?status=${filtro}` : '/chamados';
      const response = await API.get(url);
      setChamados(response.data);
    } catch (error) {
      console.error('Erro:', error);
    }
  };
  const handleCreateChamado = async (e) => {
    e.preventDefault();
    if (!cliente || !descricao) {
      alert('Preencha todos os campos');
      return;
    }
    try {
      await API.post('/chamados', {
        cliente,
        descricao,
        prioridade: parseInt(prioridade)
      });
      setCliente('');
      setDescricao('');
      setPrioridade(1);
      fetchChamados();
    } catch (error) {
      alert('Erro ao criar chamado');
    }
  };
  const handleUpdateStatus = async (id, novoStatus) => {
    try {
      await API.put(`/chamados/${id}`, { status: novoStatus });
      fetchChamados();
    } catch (error) {
      alert('Erro ao atualizar');
    }
  };
  if (!isLoggedIn) {
    return (
      <div className="login-container">
        <div className="login-box">
          <h1>🔐 Login</h1>
          <form onSubmit={handleLogin}>
            <input
              type="text"
              placeholder="Usuário"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
            />
            <input
              type="password"
              placeholder="Senha"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
            <button type="submit">Entrar</button>
          </form>
        </div>
      </div>
    );
  }
  return (
    <div className="container">
      <header>
        <h1>🔧 Gerenciamento de Chamados</h1>
        <button onClick={handleLogout} className="logout-btn">Sair</button>
      </header>
      <div className="content">
        <div className="form-section">
          <h2>Novo Chamado</h2>
          <form onSubmit={handleCreateChamado}>
            <input
              type="text"
              placeholder="Cliente"
              value={cliente}
              onChange={(e) => setCliente(e.target.value)}
            />
            <textarea
              placeholder="Descrição"
              value={descricao}
              onChange={(e) => setDescricao(e.target.value)}
            />
            <select value={prioridade} onChange={(e) => setPrioridade(e.target.value)}>
              <option value="1">Baixa</option>
              <option value="2">Média</option>
              <option value="3">Alta</option>
            </select>
            <button type="submit">Criar</button>
          </form>
        </div>
        <div className="filter-section">
          <h2>Filtrar</h2>
          <select value={filtro} onChange={(e) => setFiltro(e.target.value)}>
            <option value="">Todos</option>
            <option value="1">Aberto</option>
            <option value="2">Em Andamento</option>
            <option value="3">Finalizado</option>
          </select>
        </div>
        <div className="chamados-section">
          <h2>Chamados ({chamados.length})</h2>
          <table>
            <thead>
              <tr>
                <th>Cliente</th>
                <th>Descrição</th>
                <th>Prioridade</th>
                <th>Status</th>
                <th>Data</th>
                <th>Ação</th>
              </tr>
            </thead>
            <tbody>
              {chamados.map((c) => (
                <tr key={c.id}>
                  <td>{c.cliente}</td>
                  <td>{c.descricao}</td>
                  <td className={`priority-${c.prioridade}`}>
                    {['', 'Baixa', 'Média', 'Alta'][c.prioridade]}
                  </td>
                  <td>{['', 'Aberto', 'Em Andamento', 'Finalizado'][c.status]}</td>
                  <td>{new Date(c.dataCriacao).toLocaleDateString()}</td>
                  <td>
                    {c.status !== 3 && (
                      <select
                        value={c.status}
                        onChange={(e) => handleUpdateStatus(c.id, parseInt(e.target.value))}
                      >
                        <option value="1">Aberto</option>
                        <option value="2">Em Andamento</option>
                        <option value="3">Finalizado</option>
                      </select>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default App;