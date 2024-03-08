import { TextField } from "@mui/material";
import { axiosInstance } from "../api/axios";
import { SubmitHandler, useForm } from "react-hook-form";
import { useEffect, useState } from "react";

type Account = {
    id: number;
    number: string;
}

type CardInputs = {
    password: string;
    confirmPassword: string;
    number: string;
    cardType: string;
    activeCard: boolean;
    accountId: string;
}

interface CardPageProps{
    setIsModalVisible(visible: boolean): void;
    addCard(newCard: CardInputs): void;
}

export const CardForm = ({ setIsModalVisible, addCard}: CardPageProps) => {
    const { register, handleSubmit, setValue, reset } = useForm<CardInputs>();
    const [accountList, setAccountList] = useState<Account[]>([]);

    const fetchAccountList = async () => {
        try {
            const response = await axiosInstance.get("/api/v1/contas");
            setAccountList(response.data.$values);
        } catch (error) {
            console.error("Erro ao buscar lista de contas:", error);
        }
    };

    useEffect(() => {
        fetchAccountList()
    }, []);
    
    const handleAccountChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const selectedAccount = accountList.find(account => account.number === e.target.value);
        if (selectedAccount) {
            setValue("accountId", selectedAccount.id.toString());
        }
    };

    const onFormSubmit: SubmitHandler<CardInputs> = (data) => {
        console.log(data);

        axiosInstance.post('/api/v1/cartoes', data)
        .then((res) =>{
            console.log(res.data);
            addCard(res.data);
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

            <label htmlFor="cardType" className="block text-gray-700 font-bold mb-2">Tipo de Cartão<span className="text-red-600">*</span></label>
            <div className="mb-6 mt-4">
                <input type="radio" id="debito" checked={true} value="debito" {...register("cardType")} className="mr-2 leading-tight"/>
                <label htmlFor="debito" className="mr-4">Débito</label>
                <input type="radio" id="credito" value="credito" {...register("cardType")} className="mr-2 leading-tight"/>
                <label htmlFor="credito">Crédito</label>
            </div>


            <label htmlFor="activeCard" className="block text-gray-700 font-bold mb-2">Cartão Ativo<span className="text-red-600">*</span></label>
            <div className="mb-6 mt-4">
                <input type="radio" id="ativo" checked={true} value="true" {...register("activeCard")} className="mr-2 leading-tight"/>
                <label htmlFor="ativo" className="mr-4">Ativo</label>
                <input type="radio" id="inativo" value="false" {...register("activeCard")} className="mr-2 leading-tight"/>
                <label htmlFor="inativo">Inativo</label>
            </div>

            <label htmlFor="account" className="block text-gray-700 font-bold mb-2">Conta<span className="text-red-600">*</span></label>
            <TextField
                className="bg-white"
                id="account"
                placeholder="Digite o número da conta"
                onChange={handleAccountChange}
                fullWidth
                inputProps={{
                    autoComplete: "off",
                    list: "account-list"
                }}
            />
            <datalist id="account-list">
                {accountList && accountList.map((conta, index) => (
                    <option key={index} value={conta.number} />
                ))}
            </datalist>

            <button type="submit" className="bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 mt-4 rounded">Enviar</button>
        </form>
    );
};
