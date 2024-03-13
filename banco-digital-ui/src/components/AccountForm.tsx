import { useState, useEffect } from "react";
import { SubmitHandler, useForm } from "react-hook-form";
import { TextField } from "@mui/material";
import { axiosInstance } from "../api/axios";
import { Client } from "../types/Client";
import { Account } from "../types/Account";


type AccountInputs = {
    number: string;
    password: string;
    confirmPassword: string;
    balance: number;
    accountType: string;
    creationDate: string;
    clientId: string;
}

interface AccountPageProps {
    setIsModalVisible(visible: boolean): void;
    addAccount(newAccount: AccountInputs | Account): void;
}

export const AccountForm = ({ setIsModalVisible, addAccount }: AccountPageProps) => {
    const { register, handleSubmit, reset, getValues } = useForm<AccountInputs>();
    const [clientList, setClientList] = useState<Client[]>([]);
    const [selectedClientId, setSelectedClientId] = useState<string>("");

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
            setSelectedClientId(selectedClient.id.toString());
        }
    };

    const onFormSubmit: SubmitHandler<AccountInputs> = async () => {
        const formData = getValues();
        formData.creationDate = new Date().toISOString();
        formData.clientId = selectedClientId;
        
        await axiosInstance.post('/api/v1/contas', formData)
        .then((res) =>{
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
            <label htmlFor="number" className="block text-gray-700 font-bold mb-2">Número<span className="text-red-600">*</span></label>
            <input type="text" id="number" {...register("number")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>

            <label htmlFor="password" className="block text-gray-700 font-bold mb-2">Senha<span className="text-red-600">*</span></label>
            <input type="password" id="password" {...register("password")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>

            <label htmlFor="confirmPassword" className="block text-gray-700 font-bold mb-2">Confirmar Senha<span className="text-red-600">*</span></label>
            <input type="password" id="confirmPassword" {...register("confirmPassword")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>

            <label htmlFor="balance" className="block text-gray-700 font-bold mb-2">Saldo</label>
            <input type="number" id="balance" defaultValue={0} {...register("balance", { min: 0 })} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4"/>

            <label htmlFor="cardType" className="block text-gray-700 font-bold mb-2">Tipo de Conta<span className="text-red-600">*</span></label>
            <div className="mb-6 mt-4">
                <input type="radio" id="checkingAccount" value="checking" {...register("accountType")} className="mr-2 leading-tight"/>
                <label htmlFor="debito" className="mr-4">Corrente</label>
                <input type="radio" id="savingsAccount" value="savings" {...register("accountType")} className="mr-2 leading-tight"/>
                <label htmlFor="credito">Poupança</label>
            </div>

            <label htmlFor="client" className="block text-gray-700 font-bold mb-2">Cliente<span className="text-red-600">*</span></label>
            <TextField
                required
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
