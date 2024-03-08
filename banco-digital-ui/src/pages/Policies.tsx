import { useEffect, useState } from "react"
import { axiosInstance } from "../api/axios";
import { Table } from "../components/TableComponents/Table";
import { format } from "date-fns";
import { Modal, Tabs } from "antd";
import { PolicyForm } from "../components/PolicyForm";

interface Policy{
  id: number,
  number: string,
  hiringDate: string,
  value: number,
  driveDescription: string
}

export const Policies = () => {
  const [policies, setPolicies] = useState<Policy[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [isModalVisible, setIsModalVisible] = useState(false);

  const getPolicies = (): void => {
    setLoading(true);
    axiosInstance
      .get("/api/v1/apolices")
      .then((res) => {
        setPolicies(res.data.$values);
        setLoading(false);
      })
      .catch((error) => {
        console.log(`Erro ao buscar apólices: ${error}`)
      })
  };

  useEffect(() => {
    getPolicies();
  }, []);

  const tableHeaders = ['ID', 'Número', 'Valor', 'Data de Contratação', 'Descrição de Acionamento', 'Cartao ID'];
  const tableData = policies.map(policy => ({
    "id": policy.id,
    "numero": policy.number,
    "valor": policy.value,
    "dataContratacao": format(new Date(policy.hiringDate), 'dd/MM/yyyy'),
    "descricaoAcionamento": policy.driveDescription
  }));

  const editPolicy = (id: number): void => {
    console.log(`editar apólice ${id}`)
  };

  const deletePolicy = (id: number): void => {
    axiosInstance
      .delete(`/api/v1/apolices/${id}`)
      .then(() => {
        setPolicies(policies.filter(policy => policy.id !== id));
      })
      .catch((error) => {
        console.log(`Erro ao deletar apólice: ${error}`);
      });
  };

  const handleCreateButtonClick = (): void => {
    setIsModalVisible(true);
  };

  const handleModalClose = (): void => {
    setIsModalVisible(false);
  };

  const addPolicy = (newPolicy: any): void => {
    setPolicies([...policies, newPolicy]);
  };

  const tabsItems = [
    {
      key: "1",
      label: "Lista de Apólices",
      children: <Table
                  tableHeaders={tableHeaders}
                  tableData={tableData}
                  variableId="id"
                  editT={editPolicy}
                  deleteT={deletePolicy}
                  loading={loading}
                  buttonText="Cadastrar Apólice"
                  handleCreateButtonClick={handleCreateButtonClick}
                />
    },
    {
      key: "2",
      label: "Gerar Apólice Eletrônica"

    }
  ]

  return (
        <div className="flex justify-center pt-10">
        <div className="text-center">
          <h2 className="text-2xl font-bold mb-4">Apólices</h2>
          <Tabs defaultActiveKey="1" items={tabsItems}>
          </Tabs>
        </div>
        <Modal
          open={isModalVisible}
          onCancel={handleModalClose}
          footer={null}
        >
          <PolicyForm setIsModalVisible={setIsModalVisible} addPolicy={addPolicy}/>
        </Modal>
      </div>
  );
};