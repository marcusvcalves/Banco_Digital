import { useEffect, useState } from "react";
import { axiosInstance } from "../api/axios";
import { format } from "date-fns";
import { Table } from "../components/TableComponents/Table";
import { Modal, Tabs } from "antd";
import { AccountForm } from "../components/AccountForm";
import { Account } from "../types/Account";
import { Pix } from "../components/Pix";


export const Accounts = () => {
  const [accounts, setAccounts] = useState<Account[]>([]);
  const [selectedAccountId, setSelectedAccountId] = useState<number | null>(null);
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

  const editAccount = (id: number): void => {
    setIsModalVisible(true);
    setSelectedAccountId(id);
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
    setSelectedAccountId(null);
  };

  const addAccount = (newAccount: Account): void => {
    setAccounts([...accounts, newAccount])
  }

  const updateAccountBalance = (accountId: number, newBalance: number): void => {
    const accountIndex = accounts.findIndex((account) => account.id === accountId);
    if (accountIndex !== -1) {
      const updatedAccounts = [...accounts];
      updatedAccounts[accountIndex].balance = newBalance;
      setAccounts(updatedAccounts);
    }
  };

  const tableHeaders = ['ID', 'Número', 'Saldo', 'Tipo de Conta', 'Data de Criação', 'Nome do Cliente'];
  const tableData = accounts.map(account => ({
    "id": account.id,
    "numero": account.number,
    "saldo": `R$ ${account.balance}`,
    "tipoConta": account.accountType === "Savings" ? "Poupança" : account.accountType === "Checking" ? "Corrente" : "Comum",
    "dataCriacao": format(new Date(account.creationDate), 'dd/MM/yyyy'),
    "nomeCliente": account.client.name
  }));

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
      label: "PIX",
      children: <Pix updateAccountBalance={updateAccountBalance}/>
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
        <AccountForm 
          setIsModalVisible={setIsModalVisible}
          addAccount={addAccount}
          selectedAccountId={selectedAccountId}
          setSelectedAccountId={setSelectedAccountId}
          accounts={accounts}
          setAccounts={setAccounts}
        />
      </Modal>
    </div>
  );
};
