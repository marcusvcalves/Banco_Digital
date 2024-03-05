interface DeleteConfirmationModalProps{
    isOpen: boolean,
    onClose: () => void,
    onConfirm: () => void
}

export const DeleteConfirmationModal = ({ isOpen, onClose, onConfirm }: DeleteConfirmationModalProps) => {
    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 flex items-center justify-center bg-gray-800 bg-opacity-50">
            <div className="bg-white p-4 rounded-md shadow-md">
                <p>Deseja realmente excluir este item?</p>
                <div className="flex justify-end mt-4">
                    <button className="mr-2 bg-red-500 text-white px-4 py-2 rounded-md" onClick={onConfirm}>Confirmar</button>
                    <button className="bg-gray-300 px-4 py-2 rounded-md" onClick={onClose}>Cancelar</button>
                </div>
            </div>
        </div>
    );
};