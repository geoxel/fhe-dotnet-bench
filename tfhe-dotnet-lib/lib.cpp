#include <tfhe.h>
#include <assert.h>

///////////////////////////////////////////////////////////////////////////////

extern "C" int GenerateKeys(ClientKey **out_client_key, ServerKey **out_server_key)
{
    *out_client_key = nullptr;
    *out_server_key = nullptr;

    int outcome = 0;

    ConfigBuilder *builder = nullptr;
    outcome = config_builder_default(&builder);
    if (outcome != 0)
        return outcome;

    Config *config = nullptr;
    outcome = config_builder_build(builder, &config);
    if (outcome != 0)
        return outcome;

    ClientKey *client_key = nullptr;
    ServerKey *server_key = nullptr;
    // PublicKey *public_key = NULL;

    outcome = generate_keys(config, &client_key, &server_key);
    if (outcome != 0)
        return outcome;

    outcome = set_server_key(server_key);
    if (outcome != 0)
    {
        server_key_destroy(server_key);
        client_key_destroy(client_key);
        return outcome;
    }

    *out_client_key = client_key;
    *out_server_key = server_key;

    return 0;
}

extern "C" int ClientKey_Destroy(ClientKey *client_key)
{
    return client_key_destroy(client_key);
}

extern "C" int ServerKey_Destroy(ServerKey *server_key)
{
    return server_key_destroy(server_key);
}

extern "C" int DynamicBuffer_Destroy(DynamicBuffer *buffer)
{
    return buffer != nullptr ? destroy_dynamic_buffer(buffer) : 0;
}

///////////////////////////////////////////////////////////////////////////////

extern "C" int UInt8_Encrypt(const ClientKey *client_key, uint8_t value, FheUint8 **out_value)
{
    *out_value = nullptr;
    return fhe_uint8_try_encrypt_with_client_key_u8(value, client_key, out_value);
}

extern "C" int UInt8_Decrypt(const ClientKey *client_key, const FheUint8 *value, uint8_t *out_value)
{
    *out_value = 0;
    // fhe_uint8_try_decrypt_trivial() ?
    return fhe_uint8_decrypt(value, client_key, out_value);
}

extern "C" int UInt8_Destroy(FheUint8 *value)
{
    return fhe_uint8_destroy(value);
}

extern "C" int UInt8_Serialize(const FheUint8 *value, DynamicBuffer *out_buffer)
{
    out_buffer->pointer = nullptr;
    out_buffer->length = 0;
    out_buffer->destructor = nullptr;
    return fhe_uint8_serialize(value, out_buffer);
}

extern "C" int UInt8_Deserialize(const void *ptr, int length, FheUint8 **out_value)
{
    *out_value = nullptr;
    DynamicBufferView buffer_view = {.pointer = (const uint8_t*)ptr, .length = (size_t)length};
    return fhe_uint8_deserialize(buffer_view, out_value);
}

extern "C" int UInt8_Add(const FheUint8 *value1, const FheUint8 *value2, FheUint8 **out_value)
{
    *out_value = nullptr;
    return fhe_uint8_add(value1, value2, out_value);
}

///////////////////////////////////////////////////////////////////////////////

extern "C" int UInt32_Encrypt(const ClientKey *client_key, uint32_t value, FheUint32 **out_value)
{
    *out_value = nullptr;
    return fhe_uint32_try_encrypt_with_client_key_u32(value, client_key, out_value);
}

extern "C" int UInt32_Decrypt(const ClientKey *client_key, const FheUint32 *value, uint32_t *out_value)
{
    *out_value = 0;
    // fhe_uint32_try_decrypt_trivial() ?
    return fhe_uint32_decrypt(value, client_key, out_value);
}

extern "C" int UInt32_Destroy(FheUint32 *value)
{
    return fhe_uint32_destroy(value);
}

extern "C" int UInt32_Serialize(const FheUint32 *value, DynamicBuffer *out_buffer)
{
    out_buffer->pointer = nullptr;
    out_buffer->length = 0;
    out_buffer->destructor = nullptr;
    return fhe_uint32_serialize(value, out_buffer);
}

extern "C" int UInt32_Deserialize(const void *ptr, int length, FheUint32 **out_value)
{
    *out_value = nullptr;
    DynamicBufferView buffer_view = {.pointer = (const uint8_t*)ptr, .length = (size_t)length};
    return fhe_uint32_deserialize(buffer_view, out_value);
}

extern "C" int UInt32_Add(const FheUint32 *value1, const FheUint32 *value2, FheUint32 **out_value)
{
    *out_value = nullptr;
    return fhe_uint32_add(value1, value2, out_value);
}

///////////////////////////////////////////////////////////////////////////////
