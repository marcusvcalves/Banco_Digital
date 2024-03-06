interface CreateItemProps{
  text: string,
  handleClick: () => void
}

export const CreateItem = ( { text, handleClick }: CreateItemProps) => {
  return (
    <div className="flex">
      <button onClick={handleClick} className="inline-flex items-center px-16 py-2 bg-green-600 hover:bg-green-700 text-white font-bold rounded flex-shrink-0">
        {text}
      </button>
    </div>
  );
};
