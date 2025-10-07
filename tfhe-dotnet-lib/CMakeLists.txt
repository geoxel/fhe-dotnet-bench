cmake_minimum_required(VERSION 3.9)

project (tfhe-dotnet)
set(CMAKE_BUILD_TYPE Release)
add_library(tfhe-dotnet SHARED lib.cpp)

set(TFHE_C_API_RELEASE "${CMAKE_CURRENT_SOURCE_DIR}/../../tfhe-rs/target/release")

include_directories(${CMAKE_CURRENT_SOURCE_DIR}/include)
include_directories(${TFHE_C_API_RELEASE})

add_library(libtfhe STATIC IMPORTED)
set_target_properties(libtfhe PROPERTIES IMPORTED_LOCATION ${TFHE_C_API_RELEASE}/libtfhe.a)

target_link_libraries(tfhe-dotnet libtfhe)

if(APPLE)
    find_library(SECURITY_FRAMEWORK Security)
    if (NOT SECURITY_FRAMEWORK)
        message(FATAL_ERROR "Security framework not found")
    endif()
endif()

if(APPLE)
    target_link_libraries(tfhe-dotnet ${SECURITY_FRAMEWORK})
endif()
