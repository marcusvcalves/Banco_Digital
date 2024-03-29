import { useState, useEffect, Dispatch, SetStateAction } from "react";
import { SubmitHandler, useForm } from "react-hook-form";
import { TextField } from "@mui/material";
import { axiosInstance } from "../api/axios";
import { Card } from "../types/Card";
import { Policy } from "../types/Policy";

type PolicyInputs = {
    number: string;
    hiringDate: string;
    value: number;
    triggeringDescription: string;
    creditCardId: string;
}

interface PolicyPageProps{
    setIsModalVisible(visible: boolean): void;
    addPolicy(newPolicy: PolicyInputs | Policy): void;
    selectedPolicyId: number | null;
    setSelectedPolicyId: Dispatch<SetStateAction<number | null>>;
    policies: Policy[];
    setPolicies: Dispatch<SetStateAction<Policy[]>>;
}

export const PolicyForm = ({ setIsModalVisible, addPolicy, selectedPolicyId, setSelectedPolicyId, policies, setPolicies }: PolicyPageProps) => {
    const { register, handleSubmit, setValue, reset } = useForm<PolicyInputs>();
    const [cardList, setCardList] = useState<Card[]>([]);

    const fetchCardList = async () => {
        try {
            const response = await axiosInstance.get("/api/v1/cartoes");
            const creditCards: Card[] = response.data.$values.filter((card: Card) => card.cardType === "Credit");
            setCardList(creditCards);
        } catch (error) {
            console.error("Erro ao buscar lista de cartões:", error);
        }
    };

    useEffect(() => {
        fetchCardList()
    }, []);

    useEffect(() => {
        if (selectedPolicyId !== null){
            const selectedPolicy = policies.find(policy => policy.id === selectedPolicyId);
            if (selectedPolicy){
                setValue("number", selectedPolicy.number);
                const hiringDate = new Date(selectedPolicy.hiringDate);
                setValue("hiringDate", hiringDate.toISOString().split('T')[0]);
                setValue("value", selectedPolicy.value);
                setValue("triggeringDescription", selectedPolicy.triggeringDescription);
            }
            } else {
                reset();
            }
    }, [selectedPolicyId, policies, reset, setValue]);
    
    const handleCardChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const selectedCard = cardList.find(card => card.number === e.target.value);
        if (selectedCard) {
            setValue("creditCardId", selectedCard.id.toString());
        }
    };

    const onFormSubmit: SubmitHandler<PolicyInputs> = async (data) => {
        console.log(data);

        if (selectedPolicyId) {
            axiosInstance.put(`/api/v1/apolices/${selectedPolicyId}`, data)
                .then((res) => {
                    const updatedPolicies = policies.map(policy =>
                        policy.id === selectedPolicyId ? res.data : policy
                    );
                    setPolicies(updatedPolicies);
                    setSelectedPolicyId(null);
                    setIsModalVisible(false);
                    reset();
                })
                .catch((error) => {
                    console.log(error);
                });
        } else {
            await axiosInstance.post('/api/v1/apolices', data)
                .then((res) => {
                    addPolicy(res.data);
                    setIsModalVisible(false);
                    reset();
                })
                .catch((error) => {
                    console.log(error);
                });
        }
    };
    
    return (
        <form className="w-full max-w-md mx-auto rounded px-8 pb-4 mt-4" onSubmit={handleSubmit(onFormSubmit)}>
            <label htmlFor="number" className="block text-gray-700 font-bold mb-2">Número<span className="text-red-600">*</span></label>
            <input type="text" id="number" {...register("number")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>

            <label htmlFor="hiringDate" className="block text-gray-700 font-bold mb-2">Data de Contratação<span className="text-red-600">*</span></label>
            <input type="date" id="hiringDate" {...register("hiringDate")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>

            <label htmlFor="value" className="block text-gray-700 font-bold mb-2">Valor<span className="text-red-600">*</span></label>
            <input type="number" id="value" {...register("value")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>

            <label htmlFor="triggeringDescription" className="block text-gray-700 font-bold mb-2">Descrição de Acionamento<span className="text-red-600">*</span></label>
            <input type="text" id="triggeringDescription" {...register("triggeringDescription")} className="appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" required aria-required="true"/>


            <label htmlFor="card" className="block text-gray-700 font-bold mb-2">Cartão<span className="text-red-600">*</span></label>
            <TextField
                className="bg-white"
                id="card"
                placeholder="Digite o número do cartão"
                onChange={handleCardChange}
                fullWidth
                inputProps={{
                    autoComplete: "off",
                    list: "card-list"
                }}
            />
            <datalist id="card-list">
                {cardList && cardList.map((cartao, index) => (
                    <option key={index} value={cartao.number} />
                ))}
            </datalist>

            <button type="submit" className="bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 mt-4 rounded">Enviar</button>
        </form>
    );
};
