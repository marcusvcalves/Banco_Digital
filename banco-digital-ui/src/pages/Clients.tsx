import { Table } from "../components/TableComponents/Table";
import { axiosInstance } from "../api/axios"
import { useEffect, useState } from "react";
import { format } from 'date-fns';
import { Modal, Tabs } from "antd";
import { ClientForm } from "../components/ClientForm";

interface Client {
  id: number,
  cpf: string,
  name: string,
  birthDate: string,
  address: string,
  clientType: string
}

export const Clients = () => {
  const [clients, setClients] = useState<Client[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [isModalVisible, setIsModalVisible] = useState<boolean>(false);
  
  const getClients = (): void => {
    setLoading(true);
    axiosInstance
      .get("/api/v1/clientes")
      .then((res) => {
        setClients(res.data.$values);
        setLoading(false);
      })
      .catch((error) => {
        console.log(`Erro ao buscar clientes: ${error}`);
      })
  }

  useEffect(() => {
    getClients();
  }, []);

  const tableHeaders = ['ID', 'Nome', 'CPF', 'Data de Nascimento', 'Endereço', 'Tipo de Cliente'];
  const tableData = clients.map(client => ({
    "id": client.id,
    "nome": client.name,
    "cpf": client.cpf,
    "dataNascimento": format(new Date(client.birthDate), 'dd/MM/yyyy'),
    "endereco": client.address,
    "tipoCliente": client.clientType
  }));

  const editClient = (id: number): void => {
    console.log(`editar cliente ${id}`)
  }

  const deleteClient = (id: number): void => {
    axiosInstance
      .delete(`/api/v1/clientes/${id}`)
      .then(() => {
        setClients(clients.filter(client => client.id !== id));
      })
      .catch((error) => {
        console.log(`Erro ao deletar cliente: ${error}`);
      });
  }

  const handleCreateButtonClick = (): void => {
    setIsModalVisible(true);
  };

  const handleModalClose = (): void => {
    setIsModalVisible(false);
  };

  const addClient = (newClient: any): void => {
    setClients([...clients, newClient]);
  };

  const tabsItems = [
    {
      key: "1",
      label: "Lista de Clientes",
      children: <Table
                  tableHeaders={tableHeaders}
                  tableData={tableData}
                  variableId="id"
                  editT={editClient}
                  deleteT={deleteClient}
                  loading={loading}
                  buttonText="Cadastrar Cliente"
                  handleCreateButtonClick={handleCreateButtonClick}
                />
    }
  ]

  return (
    <div className="flex justify-center pt-10">
      <div className="text-center">
        <h2 className="text-2xl font-bold mb-4">Clientes</h2>
        <Tabs defaultActiveKey="1" items={tabsItems}>
        </Tabs>
      </div>
      <Modal
        open={isModalVisible}
        onCancel={handleModalClose}
        footer={null}
      >
        <ClientForm setIsModalVisible={setIsModalVisible} addNewClient={addClient}/>
      </Modal>
    </div>
  );
};
