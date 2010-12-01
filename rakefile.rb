desc 'default task'
task :default => [:build]

desc 'build'
task :build do
  sh 'msbuild'
end

desc 'rebuild'
task :rebuild do
  sh 'msbuild /t:clean;rebuild'
end

desc 'migrate'
task :migrate do
  cd 'bin/debug'
  sh 'Raisins.Services.Console.exe'
end
