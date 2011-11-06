desc 'default'
task :default => [:build]

desc 'start web'
task :startweb do
  cd 'src/Raisins.Client.Web'
  sh 'start WebDev.WebServer40.exe /port:3000 /path:%CD%'
end

desc 'kill web'
task :killweb do
  sh 'taskkill /IM:WebDev.WebServer40.exe'
end

desc 'build'
task :build do
  sh 'msbuild'
end

desc 'rebuild'
task :rebuild do
  sh 'msbuild /t:clean;rebuild'
end
