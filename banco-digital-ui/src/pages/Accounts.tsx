import { useEffect, useState } from "react";
import { axiosInstance } from "../api/axios";
import { format } from "date-fns";
import { Table } from "../components/TableComponents/Table";
import { Modal, Tabs } from 'antd';
import { AccountForm } from "../components/AccountForm";

interface AccountProps {
  id: number,
  number: string,
  balance: number,
  accountType: string,
  creationDate: string,
  clientId: number
}

export const Accounts = () => {
  const [accounts, setAccounts] = useState<AccountProps[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [isModalVisible, setIsModalVisible] = useState<boolean>(false);

  const getAccounts = (): void => {
    setLoading(true);
    axiosInstance
      .get("/api/v1/contas")
      .then((res) => {
        setAccounts(res.data.$values);
        setLoading(false);
      })
      .catch((error) => {
        console.log(`Erro ao buscar contas: ${error}`);
      })
  }

  useEffect(() => {
    getAccounts();
  },[]);

  const tableHeaders = ['ID', 'Número', 'Saldo', 'Tipo de Conta', 'Data de Criação', 'Cliente ID'];
  const tableData = accounts.map(account => ({
    "id": account.id,
    "numero": account.number,
    "saldo": `R$ ${account.balance}`,
    "tipoConta": account.accountType,
    "dataCriacao": format(new Date(account.creationDate), 'dd/MM/yyyy'),
    "clienteId": account.clientId
  }));

  const editAccount = (id: number): void => {
    console.log(`editar conta ${id}`)
  };

  const deleteAccount = (id: number): void => {
    axiosInstance
      .delete(`/api/v1/contas/${id}`)
      .then(() => {
        setAccounts(accounts.filter(account => account.id !== id));
      })
      .catch((error) => {
        console.log(`Erro ao deletar conta: ${error}`);
      });
  };

  const handleCreateButtonClick = (): void => {
    setIsModalVisible(true);
  };

  const handleModalClose = (): void => {
    setIsModalVisible(false);
  };

  const addAccount = (newAccount: any): void => {
    setAccounts([...accounts, newAccount])
  }

  const tabsItems = [
    {
      key: "1",
      label: "Lista de Contas",
      children: <Table       
                  tableHeaders={tableHeaders}
                  tableData={tableData}
                  variableId="id"
                  editT={editAccount}
                  deleteT={deleteAccount}
                  loading={loading}
                  buttonText="Cadastrar Conta"
                  handleCreateButtonClick={handleCreateButtonClick}
                />
    },
    {
      key: "2",
      label: "PIX"
    }
  ]

  return (
    <div className="flex justify-center pt-10">
      <div className="text-center">
        <h2 className="text-2xl font-bold mb-4">Contas</h2>
        <Tabs defaultActiveKey="1" items={tabsItems}>
        </Tabs>
      </div>
      <Modal
        open={isModalVisible}
        onCancel={handleModalClose}
        footer={null}
      >
        <AccountForm setIsModalVisible={setIsModalVisible} addAccount={addAccount}/>
      </Modal>
    </div>
  );
};
