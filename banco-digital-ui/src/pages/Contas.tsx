import { useEffect, useState } from "react"
import { axiosInstance } from "../api/axios";
import { format } from "date-fns";
import { Table } from "../components/TableComponents/Table";

interface AccountProps {
  id: number,
  saldo: number,
  dataCriacao: string,
}


export const Contas = () => {
  const [accounts, setAccounts] = useState<AccountProps[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

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

  const tableHeaders = ['ID', 'Saldo', 'Data de Criação'];
  const tableData = accounts.map(account => ({
    "id": account.id,
    "statusCartao": account.saldo,
    "dataCriacao": format(new Date(account.dataCriacao), 'dd/MM/yyyy')
  }));

  const editAccount = (id: number): void => {
    console.log(`editar cartão ${id}`)
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

  
  return (
    <div className="flex justify-center pt-10">
      <div className="text-center">
        <h2 className="text-2xl font-bold mb-4">Contas</h2>
        <Table
          tableHeaders={tableHeaders}
          tableData={tableData}
          variavelId="id"
          editT={editAccount}
          deleteT={deleteAccount}
          loading={loading}
        />
      </div>
    </div>
  );
};