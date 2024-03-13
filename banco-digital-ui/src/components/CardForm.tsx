import { TextField } from "@mui/material";
import { axiosInstance } from "../api/axios";
import { SubmitHandler, useForm } from "react-hook-form";
import { useEffect, useState } from "react";
import { Account } from "../types/Account";
import { Card } from "../types/Card";


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
    addCard(newCard: CardInputs | Card): void;
}

export const CardForm = ({ setIsModalVisible, addCard}: CardPageProps) => {
    const { register, handleSubmit, getValues, setValue, reset } = useForm<CardInputs>();
    const [accountList, setAccountList] = useState<Account[]>([]);
    const [selectedAccountId, setSelectedAccountId] = useState<string>("");
    const [selectedActiveCard, setSelectedActiveCard] = useState<string>("");
    const [selectedCardType, setSelectedCardType] = useState<string>("");

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
            setSelectedAccountId(selectedAccount.id.toString());
        }
    };
    
    const handleStatusSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const value = event.target.value;
        setSelectedActiveCard(value);
        setValue("activeCard", value === "true");
    };

    const handleCardTypeSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const value = event.target.value;
        setSelectedCardType(value);
        setValue("cardType", value);
    };

    const onFormSubmit: SubmitHandler<CardInputs> = async () => {
        const formData = getValues();
        formData.accountId = selectedAccountId;
        console.log(formData);

        await axiosInstance.post('/api/v1/cartoes', formData)
        .then((res) =>{
            addCard(res.data);
            setIsModalVisible(false);
            reset();
            setSelectedActiveCard("");
            setSelectedCardType("");
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

            <div>
                <label htmlFor="cardType" className="block text-gray-700 font-bold mb-2">
                Tipo de Cartão<span className="text-red-600">*</span>
                </label>
                <select
                    id="cardType"
                    value={selectedCardType}
                    onChange={handleCardTypeSelectChange}
                    className="border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4"
                    required
                >
                    <option value="" disabled hidden>
                        Selecione...
                    </option>
                    <option value="Debit">Débito</option>
                    <option value="Credit">Crédito</option>
                </select>
            </div>


            <div>
                <label htmlFor="activeCard" className="block text-gray-700 font-bold mb-2">
                    Status do Cartão<span className="text-red-600">*</span>
                </label>
                <select
                    id="activeCard"
                    value={selectedActiveCard}
                    onChange={handleStatusSelectChange}
                    className="border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4"
                    required
                >
                    <option value="" disabled hidden>
                        Selecione...
                    </option>
                    <option value="true">Ativo</option>
                    <option value="false">Inativo</option>
                </select>
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
