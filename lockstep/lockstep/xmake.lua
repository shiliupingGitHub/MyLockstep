
local compile_count = 0;
local function add_folder_cpp(dir_name) 
        for _, filepath in ipairs(os.files(dir_name .."/*.cpp")) do
            add_files(filepath)
            compile_count = compile_count + 1;
        end
       for _, dir in ipairs(os.dirs(dir_name .."/*")) do
             add_folder_cpp(dir)
        end
end

target("lockstep")
if is_plat("iphoneos") then
    set_kind("static")       
else
    set_kind("shared")
end
add_includedirs("third/behaviac/inc")
add_includedirs("include")
add_includedirs("third")

add_folder_cpp("src")
add_folder_cpp("third")
add_folder_cpp("include")
add_folder_cpp("wrapper")

after_build(function (target, sourcebatch, opt) 
    print("cpp files number:" .. compile_count)
    if(is_arch("arm64-v8a")) then 
        --[armeabi-v7a   x86 ]
    end 
end)
