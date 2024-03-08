import { useState, useEffect } from "react";
import { SubmitHandler, useForm } from "react-hook-form";
import { TextField } from "@mui/material";
import { axiosInstance } from "../api/axios";

type Client = {
    id: number;
    name: string;
}

type AccountInputs = {
    password: string;
    confirmPassword: string;
    balance: number;
    creationDate: string;
    clientId: string;
}

interface AccountPageProps{
    setIsModalVisible(visible: boolean): void;
    addAccount(newAccount: AccountInputs): void;
}

export const AccountForm = ({ setIsModalVisible, addAccount }: AccountPageProps) => {
    const { register, handleSubmit, setValue, reset } = useForm<AccountInputs>();
    const [clientList, setClientList] = useState<Client[]>([]);

    const fetchClientList = async () => {
        try {
            const response = await axiosInstance.get("/api/v1/clientes");
            setClientList(response.data.$values);
        } catch (error) {
            console.error("Erro ao buscar lista de clientes:", error);
        }
    };

    useEffect(() => {
        fetchClientList()
    }, []);
    
    const handleClientChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const selectedClient = clientList.find(client => client.name === e.target.value);
        if (selectedClient) {
            setValue("clientId", selectedClient.id.toString());
        }
    };

    const onFormSubmit: SubmitHandler<AccountInputs> = (data) => {
        const currentDate = new Date().toISOString();
        data.creationDate = currentDate;
        console.log(data);

        axiosInstance.post('/api/v1/contas', data)
        .then((res) =>{
            console.log(res.data);
            addAccount(res.data);
            setIsModalVisible(false);
            reset();
        })
        .catch((error) =>{
            console.log(error);
        })
        
    };
    
    return (
        <form className="w-full max-w-md mx-auto rounded px-8 pb-4 mt-4" onSubmit={handleSubmit(onFormSubmit)}>
            <label htmlFor="password" className="block text-gray-700 font-bold mb-2">Senha<span className="text-red-600">*</span></label>
            <input type="password" id="password" {...register("password")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>

            <label htmlFor="confirmPassword" className="block text-gray-700 font-bold mb-2">Confirmar Senha<span className="text-red-600">*</span></label>
            <input type="password" id="confirmPassword" {...register("confirmPassword")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>

            <label htmlFor="balance" className="block text-gray-700 font-bold mb-2">Saldo</label>
            <input type="number" id="balance" defaultValue={0} {...register("balance", { min: 0 })} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4"/>

            <label htmlFor="client" className="block text-gray-700 font-bold mb-2">Cliente<span className="text-red-600">*</span></label>
            <TextField
                className="bg-white"
                id="client"
                placeholder="Digite o nome do cliente"
                onChange={handleClientChange}
                fullWidth
                inputProps={{
                    autoComplete: "off",
                    list: "client-list"
                }}
            />
            <datalist id="client-list">
                {clientList && clientList.map((client, index) => (
                    <option key={index} value={client.name} />
                ))}
            </datalist>

            <button type="submit" className="bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 mt-4 rounded">Enviar</button>
        </form>
    );
};
