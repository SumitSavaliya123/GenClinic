namespace GenClinic_Entities.Response
{
    public class ApiResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}