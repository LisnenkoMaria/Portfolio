interface ErrorMessageProps {
    message: string;
}

export function ErrorMessage({ message }: ErrorMessageProps) {
    return (
        <div style={{
            textAlign: 'center',
            padding: '2rem',
            color: 'red',
            backgroundColor: '#ffe6e6',
            borderRadius: '8px',
            margin: '1rem'
        }}>
            Ошибка: {message}
        </div>
    );
}